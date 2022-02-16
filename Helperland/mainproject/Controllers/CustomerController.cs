using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mainproject.Models;
using System;
using System.Linq;

namespace mainproject.Controllers
{
    public class CustomerController : Controller
    {

        private readonly helperland_project1Context _database;

        public CustomerController(helperland_project1Context db)
        {
            _database = db;
        }
        public IActionResult CustomerServiceHistory()
        {

            var Id = HttpContext.Session.GetInt32("id");
            if (Id != null)
            {
                var obj = _database.Users.FirstOrDefault(x => x.UserId == Id);
                ViewBag.Name = obj.FirstName;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}