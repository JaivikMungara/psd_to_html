using mainproject.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mainproject.Controllers
{
    public class UserManagementController : Controller
    {

        private readonly helperland_project1Context _database;
        public UserManagementController(helperland_project1Context database)
        {
            _database = database;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {

                string password = _database.Users.FirstOrDefault(x => x.Email == user.username).Password;

                bool pass = BCrypt.Net.BCrypt.Verify(user.password, password);
                if (_database.Users.Where(x => x.Email == user.username && pass).Count() > 0)
                {

                    var U = _database.Users.FirstOrDefault(x => x.Email == user.username);
                    ViewBag.Name = null;

                    if (U.UserTypeId == 0)
                    {
                        if (user.remember == true)
                        {
                            CookieOptions cookieRemember = new CookieOptions();
                            cookieRemember.Expires = DateTime.Now.AddSeconds(604800);
                            Response.Cookies.Append("userId", Convert.ToString(U.UserId), cookieRemember);
                        }


                        HttpContext.Session.SetInt32("userId", U.UserId);


                        return RedirectToAction("CustomerDashboard", "Customer");
                    }
                    else if (U.UserTypeId == 1)
                    {
                        if (U.IsApproved == false)
                        {
                            ViewBag.Name = null;
                            TempData["add"] = "alert show";
                            TempData["fail"] = "You are not approved by admin, please contact admin.";
                            return RedirectToAction("Index", "Home", new { loginModal = "true" });
                        }

                        if (user.remember == true)
                        {
                            CookieOptions cookieRemember = new CookieOptions();
                            cookieRemember.Expires = DateTime.Now.AddSeconds(604800);
                            Response.Cookies.Append("userId", Convert.ToString(U.UserId), cookieRemember);
                        }


                        HttpContext.Session.SetInt32("userId", U.UserId);


                        return RedirectToAction("SPServiceRequest", "Serviceprovider");
                    }
                    /* else if (user.UserTypeId == 3)
                     {
                         return RedirectToAction("ServiceRequest", "Admin");
                     }*/

                    return RedirectToAction("CustomerDashboard", "Customer");
                }
                else
                {
                    TempData["add"] = "alert show";
                    TempData["fail"] = "username and password are invalid";
                    return RedirectToAction("Index", "Home", new { loginFail = "true" });

                }
            }

            TempData["add"] = "alert show";
            TempData["fail"] = "username and password are required";
            return RedirectToAction("Index", "Home", new { loginModal = "true" });





        }

        public IActionResult BecomeAProvider()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult BecomeAProvider(User user)
        {

            if (ModelState.IsValid)
            {
                if ((_database.Users.Where(x => x.Email == user.Email).Count() == 0 && _database.Users.Where(x => x.Mobile == user.Mobile).Count() == 0))
                {

                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.IsRegisteredUser = true;
                    user.ModifiedBy = 123;
                    user.UserTypeId = 1;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _database.Users.Add(user);
                    _database.SaveChanges();

                    TempData["add"] = "alert show";
                    TempData["msg"] = "Account created, Wait for admin approval";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["add"] = "alert show";
                    TempData["msg"] = "Username Exits!";
                    return View();
                }
            }
            return View();

        }

        public IActionResult CustomerSignUp()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CustomerSignUp(User user)
        {

            if (ModelState.IsValid)
            {
                if (_database.Users.Where(x => x.Email == user.Email).Count() == 0 && _database.Users.Where(x => x.Mobile == user.Mobile).Count() == 0)
                {

                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.IsRegisteredUser = true;
                    user.ModifiedBy = 123;
                    user.UserTypeId = 0;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);


                    _database.Users.Add(user);
                    _database.SaveChanges();

                    TempData["add"] = "alert show";
                    TempData["msg"] = "Account created, Please Login";
                    return RedirectToAction("Index", "Home", new { loginModal = "true" });


                }
                else
                {
                    TempData["add"] = "alert show";
                    TempData["msg"] = "Username Exits!";
                    return View();


                }
            }
            return View();

        }

        [HttpPost]
        public IActionResult SendMail(string email)
        {

            if (email != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.Email == email);


                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Helperland",
                "helperland12345@gmail.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(user.FirstName, email);
                message.To.Add(to);

                message.Subject = "Reset Password";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Reset your password by click below link</h1>" +
                    "<a href='" + Url.Action("ResetPassword", "UserManagement", new { userId = user.UserId }, "https") + "'>Reset Password</a>";


                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("helperland12345@gmail.com", "password@34");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return RedirectToAction("Index", "Home", new { mailSended = "true" });
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ResetPassword(int userId)
        {
            TempData["id"] = userId;
            return PartialView();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPassword rp)
        {
            rp.password = BCrypt.Net.BCrypt.HashPassword(rp.password);
            var user = new User() { UserId = rp.userId, Password = rp.password, ModifiedDate = DateTime.Now };
            _database.Users.Attach(user);
            _database.Entry(user).Property(x => x.Password).IsModified = true;
            _database.Entry(user).Property(x => x.Password).IsModified = true;
            _database.SaveChanges();


            return RedirectToAction("Index", "Home", new { loginModal = "true" });
        }
        public IActionResult logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("userId");
            return RedirectToAction("Index", "Home", new { logoutModal = "true" });
        }


    }
}

