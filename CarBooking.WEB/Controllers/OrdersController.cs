using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarBooking.DAL.EF;
using CarBooking.DAL.Entities;
using CarBooking.DAL;

namespace CarBooking.WEB.Controllers
{
    public class OrdersController : Controller
    {

        private EFUnitOfWork unitOfWork = new EFUnitOfWork();

        // GET: Orders
        public ActionResult Index()
        {
            return View(unitOfWork.Orders.GetAll().ToList());
        }

        
        // GET: Orders/Create
        public ActionResult Create(int carID)
        {
            if (Session["User"] as User != null)
            {
                ViewBag.CarID = carID;
                return View(); 
            }
            return RedirectToAction("Index", "Cars");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "StartDate,FinishDate,PassportNumber,NeedDriver,CarID")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.ClientID = (Session["User"] as User).ID;
                order.Car = unitOfWork.Cars.Get(order.CarID);
                unitOfWork.Orders.Create(order);
                try
                {
                    unitOfWork.Save();
                    return RedirectToAction("GetUserOrders");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Error when trying to save changes. Please, try again");
                    ViewBag.CarID = order.CarID;
                    return View();
                }
            }


            ViewBag.CarID = order.CarID;
            return View();
        }

        public ActionResult GetNotConfirmedAndFinished()
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                return View(unitOfWork.Orders.GetNotConfirmedAndFinished().ToList());
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult GetUserOrders()
        {
            var user = Session["User"] as User;
            if (user != null)
            {
                return View(unitOfWork.Orders.GetUserOrders(user.ID));
            }
            return RedirectToAction("Index", "Cars");
        }

        
        public ActionResult Confirm(int id)
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                unitOfWork.Orders.Confirm(id);
                unitOfWork.Save();
                return RedirectToAction("GetNotConfirmedAndFinished");
            }
            return RedirectToAction("Index", "Car");
        }

        public ActionResult Refuse(int id)
        {
            ViewBag.OrderID = id;
            return View();
        }

        [HttpPost]
        public ActionResult Refuse(int id, string comment)
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                unitOfWork.Orders.Refuse(id, comment);
                unitOfWork.Save();
                return RedirectToAction("GetNotConfirmedAndFinished");
            }
            return RedirectToAction("Index", "Car");
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            var user = Session["User"] as User;
            if (user != null)
            {
                unitOfWork.Orders.Delete(id);
                unitOfWork.Save();
                if (user.Role == Role.Client)
                {
                    return RedirectToAction("GetUserOrders");
                }
                else
                {
                    return RedirectToAction("GetNotConfirmedAndFinished");
                }
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult Pay(int id)
        {
            var user = Session["User"] as User;
            if (user != null)
            {
                unitOfWork.Orders.Pay(id);
                unitOfWork.Save();
                return RedirectToAction("GetUserOrders");
            }
            return RedirectToAction("Index", "Car");
        }

        public ActionResult ToRepair(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public ActionResult ToRepair(int id, decimal repairPrice)
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                unitOfWork.Orders.ToRepair(id, repairPrice);
                unitOfWork.Save();
                return RedirectToAction("GetNotConfirmedAndFinished");
            }
            return RedirectToAction("Index", "Car");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
