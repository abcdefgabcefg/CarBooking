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

        public void Update(Car item)
        {
            var car = db.Cars.Find(item.ID);
            car.CompanyTitle = item.CompanyTitle;
            car.IsLuxury = item.IsLuxury;
            car.Cost = item.Cost;
            car.CarTitle = item.CarTitle;
            car.IsFree = item.IsFree;
        }
    }
}
