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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                    log.Info(string.Format("Car with id = {0} was ordered", order.ID));
                    return RedirectToAction("GetUserOrders");
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    ModelState.AddModelError(string.Empty, "Error when trying to save changes. Please, try again");
                    ViewBag.CarID = order.CarID;
                    return View();
                }
            }


            ViewBag.CarID = order.CarID;
            return View();
        }

        public ActionResult DashBoard()
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                return View(unitOfWork.Orders.CreateDashBoard().ToList());
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult GetUserOrders()
        {
            var user = Session["User"] as User;
            if (user != null)
            {
                return View(unitOfWork.Orders.GetUserOrders(user.ID).ToList());
            }
            return RedirectToAction("Index", "Cars");
        }

        
        public ActionResult Confirm(int id)
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                unitOfWork.Orders.Confirm(id);
                try
                {
                    unitOfWork.Save();
                    log.Info(string.Format("Order with id = {0} was confirmed", id));
                    return RedirectToAction("DashBoard");
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    return RedirectToAction("Index", "Cars");
                }
            }
            return RedirectToAction("Index", "Cars");
        }

        [HttpPost]
        public ActionResult Refuse(int id, string comment)
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                unitOfWork.Orders.Refuse(id, comment);
                try
                {
                    unitOfWork.Save();
                    log.Info(string.Format("Order with id = {0} was refused", id));
                    return RedirectToAction("DashBoard");
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    return RedirectToAction("Index", "Cars");
                }
            }
            return RedirectToAction("Index", "Cars");
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            var user = Session["User"] as User;
            if (user != null)
            {
                unitOfWork.Orders.Delete(id);
                try
                {
                    unitOfWork.Save();
                    log.Info(string.Format("Order with id = {0} was deleted", id));
                    if (user.Role == Role.Client)
                    {
                        return RedirectToAction("GetUserOrders");
                    }
                    else
                    {
                        return RedirectToAction("DashBoard");   
                    }
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    return RedirectToAction("Index", "Cars");
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
                try
                {
                    unitOfWork.Save();
                    log.Info(string.Format("Order with id = {0} was paid", id));
                    return RedirectToAction("GetUserOrders");
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    return RedirectToAction("Index", "Cars");
                }
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult ToRepair(int id)
        { 
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public ActionResult ToRepair(int id, decimal repairPrice, string comment)
        {
            var user = Session["User"] as User;
            if (user != null && (user.Role == Role.Manager || user.Role == Role.Admin))
            {
                unitOfWork.Orders.ToRepair(id, repairPrice, comment);
                try
                {
                    unitOfWork.Save();
                    log.Info(string.Format("Car with id = {0} require repair", unitOfWork.Orders.Get(id).CarID));
                    return RedirectToAction("DashBoard");
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    return RedirectToAction("Index", "Cars");
                }
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult Update(int id)
        {
            if (Session["User"] as User != null)
            {
                return View(unitOfWork.Orders.Get(id)); 
            }
            return RedirectToAction("Index", "Cars");
        }

        [HttpPost]
        public ActionResult Update(Order order)
        {
            if (Session["User"] as User != null)
            {
                order.Car = unitOfWork.Cars.Get(order.CarID);
                unitOfWork.Orders.Update(order);
                try
                {
                    unitOfWork.Save();
                    return RedirectToAction("GetUserOrders");
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    return RedirectToAction("Index", "Cars");
                }
            }
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult PayRepair(int id)
        {
            if (Session["User"] as User != null)
            {
                unitOfWork.Orders.PayRepair(id);
                try
                {
                    unitOfWork.Save();
                    return RedirectToAction("GetUserOrders");
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}. {1}", ex.Message, ex.InnerException.Message));
                    return RedirectToAction("Index", "Cars");
                }
            }
            return RedirectToAction("Index", "Cars");
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
