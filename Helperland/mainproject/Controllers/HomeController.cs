using mainproject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mainproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly helperland_project1Context  _database;
        private readonly IWebHostEnvironment _webHostEnv;
        public HomeController( helperland_project1Context database, IWebHostEnvironment webHostEnv, ILogger<HomeController> logger)
        {
            _database = database;
            _webHostEnv = webHostEnv;
            _logger = logger;
        }
        public IActionResult Index()
        {
            int? type = null;
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _database.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                type = user.UserTypeId;

            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
                type = user.UserTypeId;
            }
            if (type != null)
            {
                if (type == 2)
                {

                    return RedirectToAction("AdminPanel", "Admin");
                }
            }
            return View();

        }
        public IActionResult Htmlpage()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _database.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;

            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
            }
            return View();

        }
        public IActionResult Contactus()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _database.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;

            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Contactus(ContactU contactu)
        {

            if (ModelState.IsValid)
            {
                if (contactu.Attach != null)
                {
                    string folder = "contactFiles/";
                    folder += Guid.NewGuid().ToString() + "_" + contactu.Attach.FileName;
                    string serverFolder = Path.Combine(_webHostEnv.WebRootPath, folder);
                    contactu.Attach.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    contactu.FileName = folder;
                    contactu.UploadFileName = contactu.Attach.FileName;
                }
                contactu.CreatedOn = DateTime.Now;
                _database.ContactUs.Add(contactu);
                contactu.Name += contactu.LastName;
                _database.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        public IActionResult Faqpage()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _database.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;

            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
            }
            return View();

        }
        public IActionResult Aboutuspage()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _database.Users.Find(id);
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;

            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _database.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                ViewBag.Name = user.FirstName;
                ViewBag.UserType = user.UserTypeId;
            }
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
