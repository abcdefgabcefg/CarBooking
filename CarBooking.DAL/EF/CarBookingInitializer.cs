using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CarBooking.DAL.Entities;

namespace CarBooking.DAL.EF
{
    class CarBookingInitializer : DropCreateDatabaseIfModelChanges<CarBookingContext>
    {
        protected override void Seed(CarBookingContext context)
        {
            var cars = new[]
            {
                new Car
                {
                    IsLuxury = true,
                    Price = 100,
                    CarTitle = "Toyota Corolla",
                    IsFree = true,
                    ImagePath = "CarPictures/Toyota Corolla.jpg"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 65,
                    CarTitle = "Volkswagen Beetle",
                    IsFree = true,
                    ImagePath = "CarPictures/Volkswagen Beetle.jpg"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 150,
                    CarTitle = "Ford Model T",
                    IsFree = true,
                    ImagePath = "CarPictures/Ford Model T.jpg"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 75,
                    CarTitle = "Ford F-Series",
                    IsFree = true,
                    ImagePath = "CarPictures/Ford F-Series.jpg"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 75,
                    CarTitle = "Ford F-Series",
                    IsFree = true,
                    ImagePath = "CarPictures/Ford F-Series.jpg"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 75,
                    CarTitle = "Ford F-Series",
                    IsFree = true,
                    ImagePath = "CarPictures/Ford F-Series.jpg"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 80,
                    CarTitle = "Chevy Silverado",
                    IsFree = true,
                    ImagePath = "CarPictures/Chevy Silverado.jpg"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 70,
                    CarTitle = "2017 Volkswagen Gulf",
                    IsFree = true,
                    ImagePath = "CarPictures/2017 Volkswagen Gulf.jpg"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 60,
                    CarTitle = "2017 Wuling Hongguang",
                    IsFree = true,
                    ImagePath = "CarPictures/2017 Wuling Hongguang.jpg"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 95,
                    CarTitle = "Honda Accord",
                    IsFree = true,
                    ImagePath = "CarPictures/Honda Accord and Honda Civic.jpg"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 90,
                    CarTitle = "Honda Civic",
                    IsFree = true,
                    ImagePath = "CarPictures/Honda Accord and Honda Civic.jpg"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 90,
                    CarTitle = "Honda Civic",
                    IsFree = true,
                    ImagePath = "CarPictures/Honda Accord and Honda Civic.jpg"
                }
            };

            var users = new[]
            {
                new User
                {
                    Login = "client",
                    Password = "123456".GetHashCode().ToString(),
                    Role = Role.Client,
                    IsBlock = false
                },

                new User
                {
                    Login = "manager",
                    Password = "123456".GetHashCode().ToString(),
                    Role = Role.Manager,
                    IsBlock = false
                },

                new User
                {
                    Login = "admin",
                    Password = "123456".GetHashCode().ToString(),
                    Role = Role.Admin,
                    IsBlock = false
                }
            };

            context.Users.AddRange(users);
            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}
