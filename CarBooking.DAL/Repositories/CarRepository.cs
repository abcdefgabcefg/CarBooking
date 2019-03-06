using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBooking.DAL.EF;
using CarBooking.DAL.Entities;

namespace CarBooking.DAL.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private CarBookingContext db;

        public CarRepository(CarBookingContext context)
        {
            db = context;
        }

        public void Create(Car item)
        {
            item.IsFree = true;
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
            return db.Cars;
        }

        public IEnumerable<Car> Get(string search, bool isLuxury, SortOrder sort, SortDirection direction)
        {
            var cars = GetAll().Where(car => car.IsFree).GroupBy(car => car.CarTitle).Select(carGroup => carGroup.FirstOrDefault()); 

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
                            case SortDirection.None:
                            case SortDirection.ASC:
                                cars = from car in cars
                                       orderby car.Price
                                       select car;
                                break;
                            case SortDirection.DESC:
                                cars = from car in cars
                                       orderby car.Price descending
                                       select car;
                                break;
                        }
                        break;
                    case SortOrder.Title:
                        switch (direction)
                        {
                            case SortDirection.None:
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
            db.Entry(item).State = EntityState.Modified;
        }
    }

    public enum SortOrder
    {
        None, Cost, Title
    }

    public enum SortDirection
    {
        None, ASC, DESC
    }
}
