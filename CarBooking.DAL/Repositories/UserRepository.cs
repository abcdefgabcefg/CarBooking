using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBooking.DAL.Entities;
using CarBooking.DAL.EF;
using System.Data.Entity;
using System.Security.Cryptography;

namespace CarBooking.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private CarBookingContext db;

        public UserRepository(CarBookingContext context)
        {
            db = context;
        }

        public void Create(User item)
        {
            if (item == null)
                throw new ArgumentNullException();

            item.IsBlock = false;
            item.Password = item.Password.GetHashCode().ToString();
            db.Users.Add(item);
        }


        public void Delete(int id)
        {
            db.Users.Remove(db.Users.Find(id));
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Block(int id)
        {
            var user = Get(id);
            user.IsBlock = true;
        }

        public void UnBlock(int id)
        {
            var user = Get(id);
            user.IsBlock = false;
        }

        public User Get(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentException();

           return (from us in GetAll()
                    where us.Login == login && us.Password == password.GetHashCode().ToString()
                    select us).FirstOrDefault();
        }

        public void CreateClient(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            user.Role = Role.Client;
            Create(user);
        }

        public void CreateManager(User manager)
        {
            if (manager == null)
                throw new ArgumentNullException();

            manager.Role = Role.Manager;
            Create(manager);
        }

        public IEnumerable<User> GetClients()
        {
            return from user in GetAll()
                   where user.Role == Role.Client
                   select user;
        }

        public IEnumerable<User> GetManagers()
        {
            return from user in GetAll()
                   where user.Role == Role.Manager
                   select user;
        }

    }
}
