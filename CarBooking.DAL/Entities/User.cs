using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBooking.DAL.Entities
{
    public class User
    {
        public int ID { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

    public enum Role
    {
        Client, Manager, Admin
    }
}
