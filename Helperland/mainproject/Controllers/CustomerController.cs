using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mainproject.Models;
using mainproject.ViewModel;
using System;
using System.Linq;
using System.Collections.Generic;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace helperland1._0.Controllers
{
    public class CustomerController : Controller
    {

        private readonly helperland_project1Context _db;

        public CustomerController(helperland_project1Context db)
        {
            _db = db;
        }

        public IActionResult CustomerDashboard()
        {

            var userTypeId = -1;
            User user = null;

            if (HttpContext.Session.GetInt32("userId") != null)
            {   user = _db.Users.Find(HttpContext.Session.GetInt32("userId"));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType    = user.UserTypeId;

                userTypeId = user.UserTypeId;
            }
            else if (Request.Cookies["userId"] != null)
            {
                user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                userTypeId = user.UserTypeId;
            }
            if (userTypeId == 0)
            {
                List<CustomerDashboard> dashboard = new List<CustomerDashboard>();
  
                //var ServiceTable = _db.ServiceRequests.Where(x => (x.UserId == user.UserId) && (x.Status == 1 || x.Status == 2)).ToList();

                var ServiceTable = _db.ServiceRequests.Where(x => x.UserId == user.UserId).ToList();

                //var ServiceTable = _db.ServiceRequests.Where(x=>x.UserId==user.UserId ).ToList();
                if (ServiceTable.Any())  /*ServiceTable.Count()>0*/
                {
                    foreach (var service in ServiceTable)
                    {

                        CustomerDashboard dash = new CustomerDashboard();
                        dash.ServiceRequestId = service.ServiceRequestId;
                        var StartDate = service.ServiceStartDate.ToString();
                        //dash.Date = StartDate.Substring(0, 10);
                        //dash.StartTime = StartDate.Substring(11);
                        dash.Date = service.ServiceStartDate.ToString("dd/MM/yyyy");
                        dash.StartTime = service.ServiceStartDate.AddHours(0).ToString("HH:mm ");
                        var totaltime = (double)(service.ServiceHours + service.ExtraHours);
                        dash.EndTime = service.ServiceStartDate.AddHours(totaltime).ToString("HH:mm ");
                        dash.Status = (int)service.Status;
                        dash.TotalCost = service.TotalCost;

                        if (service.ServiceProviderId != null)
                        {

                            User sp = _db.Users.Where(x => x.UserId == service.ServiceProviderId).FirstOrDefault();

                            dash.ServiceProvider = sp.FirstName + " " + sp.LastName;
                            dash.UserProfilePicture = "~/images/" + sp.UserProfilePicture;
                            decimal rating;

                            if (_db.Ratings.Where(x => x.RatingTo == service.ServiceProviderId).Count() > 0)
                            {
                                rating = _db.Ratings.Where(x => x.RatingTo == service.ServiceProviderId).Average(x => x.Ratings);
                            }
                            else
                            {
                                rating = 0;
                            }
                            dash.AverageRating = (float)decimal.Round(rating, 1, MidpointRounding.AwayFromZero);


                        }

                        dashboard.Add(dash);

                    }
                }

                return PartialView(dashboard);
            }


            return RedirectToAction("Index", "Home", new { loginFail = "true" });


        }

        [HttpPost]
        public String RescheduleServiceRequest(CustomerDashboard reschedule)
        {
            ServiceRequest rescheduleService = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == reschedule.ServiceRequestId);

            //Console.WriteLine(reschedule.ServiceRequestId);

            string date = reschedule.Date + " " + reschedule.StartTime;
            //Console.WriteLine(reschedule.Date);

            rescheduleService.ServiceStartDate = DateTime.Parse(date);
            rescheduleService.ServiceRequestId = reschedule.ServiceRequestId;

            if (rescheduleService.Status == 2)
            {
                int conflict = CheckConflict(rescheduleService);

                if (conflict != -1)
                {
                    ServiceRequest conflictReqObj = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == conflict);
                    DateTime conflictDateStart = conflictReqObj.ServiceStartDate;
                    DateTime ConflictDateEnd = conflictDateStart.AddMinutes((double)((conflictReqObj.ServiceHours + conflictReqObj.ExtraHours) * 60));

                    String str = "Another service request has been assigned to the service provider on " + conflictDateStart.ToString() + " to " + ConflictDateEnd.ToString() + ". Either choose another date or pick up a different time slot.";
                    return str;

                }

                User temp = _db.Users.FirstOrDefault(x => x.UserId == rescheduleService.ServiceProviderId);

                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Helperland",
                "helperland12345@gmail.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(temp.FirstName, temp.Email);
                message.To.Add(to);

                message.Subject = "Service Request Rescheduled ";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Service request with Id=" + rescheduleService.ServiceRequestId + ", has been rescheduled to " + rescheduleService.ServiceStartDate + "</ h1 > ";



                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("helperland12345@gmail.com", "password@34");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

            }

            rescheduleService.ModifiedDate = DateTime.Now;

            var result = _db.ServiceRequests.Update(rescheduleService);
            _db.SaveChanges();

            if (result != null)
            {
                return "true";
            }

            return "error";
        }

        public int CheckConflict(ServiceRequest request)
        {


            int Id = (int)request.ServiceProviderId;


            String reqdate = request.ServiceStartDate.ToString("yyyy-MM-dd");
            //Console.WriteLine(reqdate);

            String startDateStr = reqdate + " 00:00:00.000";
            String endDateStr = reqdate + " 23:59:59.999";

            //Console.WriteLine(startDateStr);

            DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd HH:mm:ss.fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

            DateTime endDate = DateTime.ParseExact(endDateStr, "yyyy-MM-dd HH:mm:ss.fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

            List<ServiceRequest> list = _db.ServiceRequests.Where(x => (x.ServiceProviderId == Id) && (x.Status == 2) && (x.ServiceStartDate > startDate && x.ServiceStartDate < endDate)).ToList();

            double mins = ((double)(request.ServiceHours + request.ExtraHours)) * 60;
            DateTime endTimeRequest = request.ServiceStartDate.AddMinutes(mins + 60);

            request.ServiceStartDate = request.ServiceStartDate.AddMinutes(-60);
            //Console.WriteLine(endTimeRequest);
            //Console.WriteLine(request.ServiceStartDate);
            foreach (ServiceRequest booked in list)
            {
                mins = ((double)(booked.ServiceHours + booked.ExtraHours)) * 60;
                DateTime endTimeBooked = booked.ServiceStartDate.AddMinutes(mins);

                if (request.ServiceStartDate < booked.ServiceStartDate)
                {
                    if (endTimeRequest <= booked.ServiceStartDate)
                    {
                        return -1;
                    }
                    else
                    {
                        return booked.ServiceRequestId;
                    }
                }
                else
                {
                    if (request.ServiceStartDate < endTimeBooked)
                    {
                        return booked.ServiceRequestId;
                    }
                }

            }



            return -1;

        }

        [HttpPost]
        public async Task<IActionResult> CancelServiceRequest(ServiceRequest cancel)
        {



            //Console.WriteLine(cancel.ServiceRequestId);
            ServiceRequest cancelService = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == cancel.ServiceRequestId);
            cancelService.Status = 4;
            if (cancel.Comments != null)
            {
                cancelService.Comments = cancel.Comments;
            }

            var result = _db.ServiceRequests.Update(cancelService);
            _db.SaveChanges();
            if (result != null)
            {

                await Task.Run(() => {

                    if (cancelService.ServiceProviderId != null)
                    {

                        User temp = _db.Users.FirstOrDefault(x => x.UserId == cancelService.ServiceProviderId);


                        MimeMessage message = new MimeMessage();

                        MailboxAddress from = new MailboxAddress("Helperland",
                        "helperland12345@gmail.com");
                        message.From.Add(from);

                        MailboxAddress to = new MailboxAddress(temp.FirstName, temp.Email);
                        message.To.Add(to);

                        message.Subject = "Service Request cancelled ";

                        BodyBuilder bodyBuilder = new BodyBuilder();
                        bodyBuilder.HtmlBody = "<h1>Service request with Id=" + cancelService.ServiceRequestId + ", has been cancled </ h1 > ";



                        message.Body = bodyBuilder.ToMessageBody();

                        SmtpClient client = new SmtpClient();
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("helperland12345@gmail.com", "password@34");
                        client.Send(message);
                        client.Disconnect(true);
                        client.Dispose();

                    }




                });




                return Ok(Json("true"));
            }

            return Ok(Json("false"));
        }

        [HttpGet]
        public JsonResult DashbordServiceDetails(CustomerDashboard ID)
        {

            CustomerDashboard Details = new CustomerDashboard();

            ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == ID.ServiceRequestId);
            Details.ServiceRequestId = ID.ServiceRequestId;
            Details.Date = sr.ServiceStartDate.ToString("dd/MM/yyyy");
            Details.StartTime = sr.ServiceStartDate.ToString("HH:mm");
            Details.Duration = (decimal)(sr.ServiceHours + sr.ExtraHours);
            Details.EndTime = sr.ServiceStartDate.AddHours((double)sr.SubTotal).ToString("HH:mm");
            Details.TotalCost = sr.TotalCost;
            Details.Comments = sr.Comments;
            Details.Status = (int)sr.Status;

            //Console.WriteLine("helo");
            //Console.WriteLine(Details.Status);
            List<ServiceRequestExtra> SRExtra = _db.ServiceRequestExtras.Where(x => x.ServiceRequestId == ID.ServiceRequestId).ToList();

            foreach (ServiceRequestExtra row in SRExtra)
            {
                if (row.ServiceExtraId == 1)
                {
                    Details.Cabinet = true;
                }
                else if (row.ServiceExtraId == 2)
                {
                    Details.Oven = true;
                }
                else if (row.ServiceExtraId == 3)
                {
                    Details.Window = true;
                }
                else if (row.ServiceExtraId == 4)
                {
                    Details.Fridge = true;
                }
                else
                {
                    Details.Laundry = true;
                }
            }

            ServiceRequestAddress Address = _db.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == ID.ServiceRequestId);

            Details.Address = Address.AddressLine1 + ", " + Address.AddressLine2 + ", " + Address.City + " - " + Address.PostalCode;

            Details.PhoneNo = Address.Mobile;
            Details.Email = Address.Email;

            return new JsonResult(Details);
        }

        [HttpGet]
        public JsonResult GetRating(CustomerDashboard ID)
        {
            ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == ID.ServiceRequestId);

            if (_db.Ratings.Where(x => x.RatingTo == sr.ServiceProviderId).Count() > 0)
            {
                decimal avgrating = _db.Ratings.Where(x => x.RatingTo == sr.ServiceProviderId).Average(x => x.Ratings);

                CustomerDashboard customerDashboard = new CustomerDashboard();
                customerDashboard.AverageRating = (float)decimal.Round(avgrating, 1, MidpointRounding.AwayFromZero);

                User sp = _db.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();
                customerDashboard.UserProfilePicture = "~/images/" + sp.UserProfilePicture;
                customerDashboard.ServiceProvider = sp.FirstName + " " + sp.LastName;

                return new JsonResult(customerDashboard);
            }
            return new JsonResult(null);
        }


        public IActionResult RateServiceProvider(Rating rating)
        {
            int? Id = -1;
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                Id = HttpContext.Session.GetInt32("userId");
            }
            else if (Request.Cookies["userId"] != null)
            {

                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }

            if (Id != null)
            {
                if (_db.Ratings.Where(x => x.ServiceRequestId == rating.ServiceRequestId).Count() > 0)
                {
                    return Ok(Json("false"));
                }


                rating.RatingDate = DateTime.Now;
                ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == rating.ServiceRequestId);
                rating.RatingTo = (int)sr.ServiceProviderId;
                rating.RatingFrom = (int)Id;
                //Console.WriteLine(rating.Ratings);

                var result = _db.Ratings.Add(rating);
                _db.SaveChanges();

                if (result != null)
                {
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));
        }

        /*Book service*/

        public IActionResult BookService()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _db.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                if (user.UserTypeId == 0)
                {
                    return PartialView();
                }
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                if (user.UserTypeId == 0)
                {
                    return PartialView();
                }
            }
            TempData["add"] = "alert show";
            TempData["fail"] = "Please Login to book service";
            return RedirectToAction("Index", "Home", new { loginFail = "true" });


        }

        [HttpPost]
        public IActionResult ValidPostalCode(PostalCode obj)
        {
            if (ModelState.IsValid)
            {

                var list = _db.Users.Where(x => (x.ZipCode == obj.postalcode) && (x.UserTypeId == 1)).ToList();


                if (list.Count() > 0)
                {


                    return Ok(Json("true"));
                }
                // TempData["wrongZipCode"] = "Postal code you have entered is not valid.";
                return Ok(Json("false"));
            }
            else
            {
                return Ok(Json("Invalid"));
            }
        }

        [HttpPost]
        public ActionResult ScheduleService(ScheduleService schedule)
        {

            if (ModelState.IsValid)
            {


                return Ok(Json("true"));


            }
            else
            {

                return Ok(Json("false"));
            }



        }


        [HttpGet]
        public IActionResult DetailsService(PostalCode obj)
        {

            int Id = -1;

            List<Address> Addresses = new List<Address>();
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                Id = (int)HttpContext.Session.GetInt32("userId");
            }
            else if (Request.Cookies["userId"] != null)
            {
                Id = int.Parse(Request.Cookies["userId"]);

            }


            string postalcode = obj.postalcode;
            //Console.WriteLine(obj.postalcode);
            var table = _db.UserAddresses.Where(x => x.UserId == Id && x.PostalCode == postalcode).ToList();
            //Console.WriteLine(table.ToString());

            foreach (var add in table)
            {
                //Console.WriteLine("1");
                Address useradd = new Address();
                useradd.AddressId = add.AddressId;
                useradd.AddressLine1 = add.AddressLine1;
                useradd.AddressLine2 = add.AddressLine2;
                useradd.City = add.City;
                useradd.PostalCode = add.PostalCode;
                useradd.Mobile = add.Mobile;
                useradd.isDefault = add.IsDefault;

                Addresses.Add(useradd);
            }
            //Console.WriteLine("2");

            return new JsonResult(Addresses);
        }


        [HttpPost]
        public IActionResult AddNewAddress(UserAddress useradd)
        {

            if (ModelState.IsValid)
            {


                //Console.WriteLine("Inside Addnew address 1");
                int Id = -1;


                if (HttpContext.Session.GetInt32("userId") != null)
                {
                    Id = (int)HttpContext.Session.GetInt32("userId");
                }
                else if (Request.Cookies["userId"] != null)
                {
                    Id = int.Parse(Request.Cookies["userId"]);

                }
                //Console.WriteLine("Inside Addnew address 2");
                //Console.WriteLine(Id);

                useradd.UserId = Id;
                useradd.IsDefault = false;
                useradd.IsDeleted = false;
                User user = _db.Users.Where(x => x.UserId == Id).FirstOrDefault();
                useradd.Email = user.Email;
                var result = _db.UserAddresses.Add(useradd);
                //Console.WriteLine("Inside Addnew address 3");
                _db.SaveChanges();

                //Console.WriteLine("Inside Addnew address 4");
                if (result != null)
                {
                    return Ok(Json("true"));
                }

                return Ok(Json("false"));

            }
            return View();
        }

        public ActionResult CompleteBooking(CompleteBooking complete)
        {
            int Id = -1;


            if (HttpContext.Session.GetInt32("userId") != null)
            {
                Id = (int)HttpContext.Session.GetInt32("userId");
            }
            else if (Request.Cookies["userId"] != null)
            {
                Id = int.Parse(Request.Cookies["userId"]);

            }


            ServiceRequest add = new ServiceRequest();
            add.UserId = Id;
            add.ServiceId = Id;
            add.ServiceStartDate = complete.ServiceStartDate;
            add.ServiceHours = (double)complete.ServiceHours;
            add.ZipCode = complete.PostalCode;
            add.ServiceHourlyRate = 25;
            add.ExtraHours = complete.ExtraHours;
            add.SubTotal = (decimal)complete.SubTotal;
            add.TotalCost = (decimal)complete.TotalCost;
            add.Comments = complete.Comments;
            add.PaymentDue = false;
            add.PaymentDone = true;
            add.HasPets = complete.HasPets;
            add.CreatedDate = DateTime.Now;
            add.ModifiedDate = DateTime.Now;
            add.HasIssue = false;
            add.Status = 1;

            var result = _db.ServiceRequests.Add(add);
            _db.SaveChanges();

            UserAddress useraddr = _db.UserAddresses.Where(x => x.AddressId == complete.AddressId).FirstOrDefault();

            ServiceRequestAddress srAddr = new ServiceRequestAddress();
            srAddr.AddressLine1 = useraddr.AddressLine1;
            srAddr.AddressLine2 = useraddr.AddressLine2;
            srAddr.City = useraddr.City;
            srAddr.Email = useraddr.Email;
            srAddr.Mobile = useraddr.Mobile;
            srAddr.PostalCode = useraddr.PostalCode;
            srAddr.ServiceRequestId = result.Entity.ServiceRequestId;
            srAddr.State = useraddr.State;

            var srAddrResult = _db.ServiceRequestAddresses.Add(srAddr);
            _db.SaveChanges();

            if (complete.Cabinet == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 1;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Fridge == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 2;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Oven == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 3;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Laundry == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 4;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Window == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 5;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }

            if (result != null && srAddrResult != null)
            {
                sendServiceMailtoSP(result.Entity.ServiceRequestId);
                return Ok(Json(result.Entity.ServiceRequestId));


            }

            return Ok(Json("false"));
        }

        public async Task sendServiceMailtoSP(int serviceId)
        {
            int Id = -1;


            if (HttpContext.Session.GetInt32("userId") != null)
            {
                Id = (int)HttpContext.Session.GetInt32("userId");
            }
            else if (Request.Cookies["userId"] != null)
            {
                Id = int.Parse(Request.Cookies["userId"]);

            }

            var serviceProviderList = _db.Users.Where(x => x.UserTypeId == 1 && x.IsApproved == true).ToList();
            var BlockedBySp = _db.FavoriteAndBlockeds.Where(x => x.TargetUserId == Id && x.IsBlocked == true).Select(x => x.UserId).ToList();

            var SpByBlocked = _db.FavoriteAndBlockeds.Where(x => x.TargetUserId == Id && x.IsBlocked == true).Select(x => x.UserId).ToList();

            //BlockedBySp.AddRange(SpBlockedByCust);
            var blocked = BlockedBySp;

            await Task.Run(() =>
            {
                foreach (var temp in serviceProviderList)
                {
                    if (!SpByBlocked.Contains(temp.UserId))
                    {
                        MimeMessage message = new MimeMessage();

                        MailboxAddress from = new MailboxAddress("Helperland",
                        "helperland12345@gmail.com");
                        message.From.Add(from);

                        MailboxAddress to = new MailboxAddress(temp.FirstName, temp.Email);
                        message.To.Add(to);

                        message.Subject = "New Service Request";

                        BodyBuilder bodyBuilder = new BodyBuilder();
                        bodyBuilder.HtmlBody = "<h1>A new service request is created with ID number " + serviceId + "</h1>";



                        message.Body = bodyBuilder.ToMessageBody();

                        SmtpClient client = new SmtpClient();
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("helperland12345@gmail.com", "password@34");
                        client.Send(message);
                        client.Disconnect(true);
                        client.Dispose();
                    }
                }


            });

        }

        [HttpGet]
        public JsonResult GetCustomerData()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }

            User user = _db.Users.FirstOrDefault(x => x.UserId == Id);
            return new JsonResult(user);

        }

        [HttpPost]
        public IActionResult UpdateCustomerData(User user)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }
            User u = _db.Users.FirstOrDefault(x => x.UserId == Id);
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Mobile = user.Mobile;
            u.DateOfBirth = user.DateOfBirth;
            u.ModifiedDate = DateTime.Now;

            var result = _db.Users.Update(u);
            _db.SaveChanges();
            if (result != null)
            {
                return Ok(Json("true"));
            }

            return Ok(Json("false"));
        }

        [HttpGet]
        public JsonResult GetUserAddress()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }

            List<UserAddress> Addresses = _db.UserAddresses.Where(x => x.UserId == Id && x.IsDeleted == false).ToList();
            return new JsonResult(Addresses);

        }

        [HttpPost]
        public JsonResult DeleteUserAddress(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }
            UserAddress userAddress = _db.UserAddresses.FirstOrDefault(x => x.AddressId == addr.AddressId);

            userAddress.IsDeleted = true;
            var result = _db.UserAddresses.Update(userAddress);
            _db.SaveChanges();
            if (result != null)
            {
                return new JsonResult(true);
            }
            else
            {

                return new JsonResult(false);
            }
        }

        /*----- Add User Address -----*/
        public IActionResult AddNewUserAddress(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }
            addr.UserId = (int)Id;
            addr.IsDefault = false;
            addr.IsDeleted = false;
            var result = _db.UserAddresses.Add(addr);
            _db.SaveChanges();
            if (result != null)
            {
                return Ok(Json("true"));
            }
            else
            {
                return Ok(Json("false"));
            }

        }

        [HttpGet]
        public JsonResult EditAddressModel(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }
            UserAddress address = _db.UserAddresses.FirstOrDefault(x => x.AddressId == addr.AddressId);
            return new JsonResult(address);


        }

        [HttpPost]
        public IActionResult EditUserAddress(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }
            addr.UserId = (int)Id;
            var result = _db.UserAddresses.Update(addr);
            _db.SaveChanges();
            if (result != null)
            {
                return Ok(Json("true"));
            }
            else
            {
                return Ok(Json("false"));
            }
        }

        /*change password mysettings */

       public IActionResult ChangePassword(ChangePassword password)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }
            User user = _db.Users.FirstOrDefault(x => x.UserId == Id);


            if (BCrypt.Net.BCrypt.Verify(password.oldPassword, user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password.newPassword);
                user.ModifiedDate = DateTime.Now;
                _db.Users.Update(user);
                _db.SaveChanges();
                return Ok(Json("true"));
            }
            else
            {
                return Ok(Json("wrong password"));
            }


        }

        //Block Sp

        public JsonResult getSP()
        {

            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }

            List<int?> SPID = _db.ServiceRequests.Where(x => x.UserId == Id && x.Status == 3).Select(u => u.ServiceProviderId).ToList();


            var SPSetId = new HashSet<int?>(SPID);

            List<BlockCustomerData> blockData = new List<BlockCustomerData>();

            foreach (int temp in SPSetId)
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == temp);
                FavoriteAndBlocked FB = _db.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == Id && x.TargetUserId == temp);

                BlockCustomerData blockCustomerData = new BlockCustomerData();
                blockCustomerData.user = user;
                blockCustomerData.favoriteAndBlocked = FB;

                blockData.Add(blockCustomerData);



            }



            return Json(blockData);


        }


        public string BlockUnblockFavUnFavSp(BlockDTO temp)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null)
            {
                Id = Convert.ToInt32(Request.Cookies["userId"]);
            }

            FavoriteAndBlocked obj = _db.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == Id && x.TargetUserId == temp.Id);

            if (temp.Req == "B")
            {

                if (obj == null)
                {
                    obj = new FavoriteAndBlocked();
                    obj.UserId = (int)Id;
                    obj.TargetUserId = temp.Id;
                    obj.IsBlocked = true;

                }
                else
                {
                    obj.IsBlocked = true;
                }

            }
            else if (temp.Req == "U")
            {
                obj.IsBlocked = false;

            }



            if (temp.Req == "F")
            {

                if (obj == null)
                {
                    obj = new FavoriteAndBlocked();
                    obj.UserId = (int)Id;
                    obj.TargetUserId = temp.Id;
                    obj.IsFavorite = true;

                }
                else
                {
                    obj.IsFavorite = true;
                }

            }
            else if (temp.Req == "N")
            {
                obj.IsFavorite = false;

            }




            var result = _db.FavoriteAndBlockeds.Update(obj);
            _db.SaveChanges();
            if (result != null)
            {
                return "Suceess";
            }
            else
            {
                return "error";
            }


        }


    }
}