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
          if (_database.Users.Where(x => x.Email == user.username && x.Password == user.password).Count() > 0)
        {

          var U = _database.Users.FirstOrDefault(x => x.Email == user.username);
        HttpContext.Session.SetInt32("id", U.UserId);

        return RedirectToAction("CustomerServiceHistory", "Customer");
        }
        else
        {
          TempData["add"] = "alert show";
        TempData["fail"] = "something is wrong.username and password are invalid";
        return RedirectToAction("Index", "Home", new { loginFail = "true" });

        }
        }

        TempData["add"] = "alert show";
        TempData["fail"] = "something is wrong.username and password are required";
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
            var user = new User() { UserId = rp.userId, Password = rp.password, ModifiedDate = DateTime.Now };
            _database.Users.Attach(user);
            _database.Entry(user).Property(x => x.Password).IsModified = true;
            _database.Entry(user).Property(x => x.Password).IsModified = true;
            _database.SaveChanges();


            return RedirectToAction("Index", "Home", new { loginModal = "true" });
        }

    }
}

