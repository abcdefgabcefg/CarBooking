using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBooking.DAL.EF;
using CarBooking.DAL.Entities;

namespace CarBooking.DAL.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private CarBookingContext db = new CarBookingContext();

        public void Create(Car item)
        {
            db.Cars.Add(item);
        }

        public void Delete(int id)
        {
            db.Cars.Remove(db.Cars.Find(id));
        }

        public Car Get(int id)
        {
            return db.Cars.Find(id);
        }

        public IEnumerable<Car> GetAll()
        {
            return db.Cars.Where(car => car.IsFree).Distinct(new CarEqualityComparer());
        }

        public IEnumerable<Car> Get(string search = "", bool isLuxury = false, SortOrder sort = SortOrder.None, SortDirection direction = SortDirection.ASC)
        {
            var cars = GetAll();

            if(!string.IsNullOrWhiteSpace(search))
            {
                cars = from car in cars
                       where car.CarTitle.ToLower().Contains(search.Trim().ToLower())
                       select car;
            }

            if (isLuxury)
            {
                cars = from car in cars
                       where car.IsLuxury
                       select car;
            }

            if(sort != SortOrder.None)
            {
                switch (sort)
                {
                    case SortOrder.Cost:
                        switch (direction)
                        {
                            case SortDirection.ASC:
                                cars = from car in cars
                                       orderby car.Cost
                                       select car;
                                break;
                            case SortDirection.DESC:
                                cars = from car in cars
                                       orderby car.Cost descending
                                       select car;
                                break;
                        }
                        break;
                    case SortOrder.Title:
                        switch (direction)
                        {
                            case SortDirection.ASC:
                                cars = from car in cars
                                       orderby car.CarTitle
                                       select car;
                                break;
                            case SortDirection.DESC:
                                cars = from car in cars
                                       orderby car.CarTitle descending
                                       select car;
                                break;
                        }
                        break;
                }
            }
            return cars;
        }

        public void Update(Car item)
        {
            var car = db.Cars.Find(item.ID);
            car.IsLuxury = item.IsLuxury;
            car.Cost = item.Cost;
            car.CarTitle = item.CarTitle;
            car.IsFree = item.IsFree;
        }
    }

    public enum SortOrder
    {
        None, Cost, Title
    }

    public enum SortDirection
    {
        ASC, DESC
    }
}
