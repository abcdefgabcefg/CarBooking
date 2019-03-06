using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarBooking.DAL.Entities;
using CarBooking.DAL;

namespace CarBooking.WEB.Controllers
{
    public class UsersController : Controller
    {
        private EFUnitOfWork unitOfWork = new EFUnitOfWork();

        public ActionResult Register()
        {
            ViewBag.Title = "Sign Up";
            return View("InputUser");
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!unitOfWork.Users.IsUnique(user.Login))
            {
                ModelState.AddModelError("Login", "It is busy login");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Users.CreateClient(user);
                try
                {
                    unitOfWork.Save();
                    Session["User"] = user;
                    return RedirectToAction("GetUserOrders", "Orders");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Error when trying to save changes. Please, try again");
                    return View("InputUser");
                }
            }
            return View("InputUser");
        }

        public ActionResult CreateManager()
        {
            ViewBag.Title = "Create Manager";
            return View("InputUser");
        }

        [HttpPost]
        public ActionResult CreateManager(User user)
        {
            if (!unitOfWork.Users.IsUnique(user.Login))
            {
                ModelState.AddModelError("Login", "It is busy login");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Users.CreateManager(user);
                try
                {
                    unitOfWork.Save();
                    return RedirectToAction("GetManagers");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Error when trying to save changes. Please, try again");
                    return View("InputUser");
                }
            }
            return View("InputUser");
        }

        public ActionResult LogIn()
        {
            ViewBag.Title = "Log In";
            return View("InputUser");
        }

        [HttpPost]
        public ActionResult LogIn(string login, string password)
        {
            var user = unitOfWork.Users.Get(login, password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Such user not found");
            }
            else if(user.IsBlock)
            {
                ModelState.AddModelError(string.Empty, "Such user is block");
            }
            if (ModelState.IsValid)
            {
                Session["User"] = user;
                switch (user.Role)
                {
                    case Role.Client:
                        return RedirectToAction("GetUserOrders", "Orders");
                    case Role.Manager:
                        return RedirectToAction("GetNotConfirmedAndFinished", "Orders");
                    case Role.Admin:
                        return RedirectToAction("GetAll", "Cars");
                    default:
                        return RedirectToAction("Index", "Cars");
                }
            }
            return View("InputUser");
        }

        public ActionResult BlockUser(int id)
        {
            var user = Session["User"] as User;
            if (user !=  null && user.Role == Role.Admin)
            {
                unitOfWork.Users.Block(id);
                unitOfWork.Save();
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult UnBlockUser(int id)
        {
            var user = Session["User"] as User;
            if (user != null && user.Role == Role.Admin)
            {
                unitOfWork.Users.UnBlock(id);
                unitOfWork.Save();
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("LogIn");
        }

        public ActionResult GetUsers()
        {
            var user = Session["User"] as User;
            if (user != null && user.Role == Role.Admin)
            {
                return View(unitOfWork.Users.GetClients().ToList());
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult GetManagers()
        {
            var user = Session["User"] as User;
            if (user != null && user.Role == Role.Admin)
            {
                return View(unitOfWork.Users.GetManagers().ToList());
            }
            return RedirectToAction("Index", "Cars");
        }
    }
}