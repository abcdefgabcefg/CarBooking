using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarBooking.DAL.Entities;
using CarBooking.DAL;

namespace CarBooking.WEB.Controllers
{
    public class UserController : Controller
    {
        private EFUnitOfWork unitOfWork = new EFUnitOfWork();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClient(User user)
        {
            user.Role = Role.Client;
            unitOfWork.Users.Create(user);
            unitOfWork.Save();
            return RedirectToAction("Index", "Cars");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateManager(User user)
        {
            user.Role = Role.Manager;
            unitOfWork.Users.Create(user);
            unitOfWork.Save();
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string login, string password)
        {
            var user = (from us in unitOfWork.Users.GetAll()
                        where us.Login == login && us.Password == password
                        select us).FirstOrDefault();
            if(user != null)
            {
                Session["User"] = user;
                return RedirectToAction("Index", "Cars");
            }
            return RedirectToAction("LogIn");
        }
    }
}