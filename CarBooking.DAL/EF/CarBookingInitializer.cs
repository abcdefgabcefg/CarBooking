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
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 65,
                    CarTitle = "Volkswagen Beetle",
                    IsFree = true,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 150,
                    CarTitle = "Ford Model T",
                    IsFree = true,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 75,
                    CarTitle = "Ford F-Series",
                    IsFree = false,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 75,
                    CarTitle = "Ford F-Series",
                    IsFree = true,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 75,
                    CarTitle = "Ford F-Series",
                    IsFree = true,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 80,
                    CarTitle = "Chevy Silverado",
                    IsFree = false,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 70,
                    CarTitle = "2017 Volkswagen Gulf",
                    IsFree = true,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = false,
                    Price = 60,
                    CarTitle = "2017 Wuling Hongguang",
                    IsFree = true,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 95,
                    CarTitle = "Honda Accord",
                    IsFree = true,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 90,
                    CarTitle = "Honda Civic",
                    IsFree = false,
                    ImagePath = "Here is URL"
                },

                new Car
                {
                    IsLuxury = true,
                    Price = 90,
                    CarTitle = "Honda Civic",
                    IsFree = false,
                    ImagePath = "Here is URL"
                }
            };

            var users = new[]
            {
                new User
                {
                    Login = "tree17",
                    Password = "123456",
                    Role = Role.Client
                },

                new User
                {
                    Login = "frog65",
                    Password = "123456",
                    Role = Role.Manager
                },

                new User
                {
                    Login = "sky88",
                    Password = "123456",
                    Role = Role.Admin
                }
            };

            var orders = new[]
            {
                new Order
                {
                    StartDate = new DateTime(2019, 03, 22, 16, 00, 00),
                    FinishDate = new DateTime(2019, 03, 26, 12, 00, 00),
                    PassportNumber = "535565031",
                    NeedDriver = false,
                    CarID = 4,
                    ClientID = 1,
                    ManagerID = 2,
                    Status = Status.NotConfirmed
                },

                new Order
                {
                    StartDate = new DateTime(2019, 03, 25, 11, 00, 00),
                    FinishDate = new DateTime(2019, 04, 01, 22, 00, 00),
                    PassportNumber = "168415759",
                    NeedDriver = true,
                    CarID = 7,
                    ClientID = 1,
                    ManagerID = 2,
                    Status = Status.NotConfirmed
                },

                new Order
                {
                    StartDate = new DateTime(2019, 03, 28, 02, 00, 00),
                    FinishDate = new DateTime(2019, 04, 11, 07, 00, 00),
                    PassportNumber = "985351351",
                    NeedDriver = true,
                    CarID = 11,
                    ClientID = 1,
                    ManagerID = 2,
                    Status = Status.NotConfirmed
                },

                new Order
                {
                    StartDate = new DateTime(2019, 04, 15, 13, 00, 00),
                    FinishDate = new DateTime(2019, 04, 22, 06, 00, 00),
                    PassportNumber = "568636513",
                    NeedDriver = false,
                    CarID = 12,
                    ClientID = 1,
                    ManagerID = 2,
                    Status = Status.NotConfirmed
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            context.Cars.AddRange(cars);
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
