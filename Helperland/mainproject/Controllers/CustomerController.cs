using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mainproject.Models;
using System;
using System.Linq;
using mainproject.ViewModel;
using System.Collections.Generic;

namespace mainproject.Controllers
{
    public class CustomerController : Controller
    {

        private readonly helperland_project1Context _database;


        public CustomerController(helperland_project1Context database)
        {
            _database = database;
        }

        public IActionResult CustomerServiceHistory()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _database.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                if (user.UserTypeId == 0)
                {
                    return PartialView();
                }


            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                if (user.UserTypeId == 0)
                {
                    return PartialView();
                }
            }
            return RedirectToAction("Index", "Home");


        }





        public IActionResult BookService()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _database.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                if (user.UserTypeId == 0)
                {
                    return PartialView();
                }
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                if (user.UserTypeId == 0)
                {
                    return PartialView();
                    //return PartialView();
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

                var list = _database.Users.Where(x => (x.ZipCode == obj.postalcode) && (x.UserTypeId == 1)).ToList();


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
            Console.WriteLine(obj.postalcode);
            var table = _database.UserAddresses.Where(x => x.UserId == Id && x.PostalCode == postalcode).ToList();
            Console.WriteLine(table.ToString());

            foreach (var add in table)
            {
                Console.WriteLine("1");
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
            Console.WriteLine("2");

            return new JsonResult(Addresses);
        }

        [HttpPost]
        public IActionResult AddNewAddress(UserAddress useradd)
        {

            if (ModelState.IsValid)
            {


                Console.WriteLine("Inside Addnew address 1");
                int Id = -1;


                if (HttpContext.Session.GetInt32("userId") != null)
                {
                    Id = (int)HttpContext.Session.GetInt32("userId");
                }
                else if (Request.Cookies["userId"] != null)
                {
                    Id = int.Parse(Request.Cookies["userId"]);

                }
                Console.WriteLine("Inside Addnew address 2");
                Console.WriteLine(Id);

                useradd.UserId = Id;
                useradd.IsDefault = false;
                useradd.IsDeleted = false;
                User user = _database.Users.Where(x => x.UserId == Id).FirstOrDefault();
                useradd.Email = user.Email;
                var result = _database.UserAddresses.Add(useradd);
                Console.WriteLine("Inside Addnew address 3");
                _database.SaveChanges();

                Console.WriteLine("Inside Addnew address 4");
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

            var result = _database.ServiceRequests.Add(add);
            _database.SaveChanges();

            UserAddress useraddr = _database.UserAddresses.Where(x => x.AddressId == complete.AddressId).FirstOrDefault();

            ServiceRequestAddress srAddr = new ServiceRequestAddress();
            srAddr.AddressLine1 = useraddr.AddressLine1;
            srAddr.AddressLine2 = useraddr.AddressLine2;
            srAddr.City = useraddr.City;
            srAddr.Email = useraddr.Email;
            srAddr.Mobile = useraddr.Mobile;
            srAddr.PostalCode = useraddr.PostalCode;
            srAddr.ServiceRequestId = result.Entity.ServiceRequestId;
            srAddr.State = useraddr.State;

            var srAddrResult = _database.ServiceRequestAddresses.Add(srAddr);
            _database.SaveChanges();

            if (complete.Cabinet == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 1;
                _database.ServiceRequestExtras.Add(srExtra);
                _database.SaveChanges();
            }
            if (complete.Fridge == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 2;
                _database.ServiceRequestExtras.Add(srExtra);
                _database.SaveChanges();
            }
            if (complete.Oven == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 3;
                _database.ServiceRequestExtras.Add(srExtra);
                _database.SaveChanges();
            }
            if (complete.Laundry == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 4;
                _database.ServiceRequestExtras.Add(srExtra);
                _database.SaveChanges();
            }
            if (complete.Window == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 5;
                _database.ServiceRequestExtras.Add(srExtra);
                _database.SaveChanges();
            }



            if (result != null && srAddrResult != null)
            {
                return Ok(Json(result.Entity.ServiceRequestId));
            }

            return Ok(Json("false"));
        }



    }
}