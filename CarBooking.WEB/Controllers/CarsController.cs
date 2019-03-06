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
using CarBooking.DAL.Repositories;

namespace CarBooking.WEB.Controllers
{
    public class CarsController : Controller
    {
        private EFUnitOfWork unitOfWork = new EFUnitOfWork();

        // GET: Cars
        public ActionResult Index(string search, bool? isLuxury, SortOrder? sortOrder, SortDirection? sortDirection)
        {
            if (search == null)
            {
                search = string.Empty;
            }
            return View(unitOfWork.Cars.Get(search, isLuxury.GetValueOrDefault(), sortOrder.GetValueOrDefault(), sortDirection.GetValueOrDefault()).ToList());
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            var user = Session["User"] as User;
            if (user != null && user.Role == Role.Admin)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "IsLuxury,Price,CarTitle,ImagePath")] Car car)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Cars.Create(car);
                try
                {
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Error when trying to save changes. Please, try again");
                    return View();
                }
            }

            return View();
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = Session["User"] as User;
            if (user != null && user.Role == Role.Admin)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Car car = unitOfWork.Cars.Get(id.Value);
                if (car == null)
                {
                    return HttpNotFound();
                }
                return View(car); 
            }
            return RedirectToAction("Index");
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,IsLuxury,Price,CarTitle,ImagePath")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.IsFree = true;
                unitOfWork.Cars.Update(car);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        public ActionResult Delete(int id)
        {
            unitOfWork.Cars.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult GetAll()
        {
            var user = Session["User"] as User;
            if (user != null && user.Role == Role.Admin)
            {
                return View(unitOfWork.Cars.GetAll().ToList()); 
            }
            return RedirectToAction("Index");
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
