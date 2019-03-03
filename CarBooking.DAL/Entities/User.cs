using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarBooking.DAL.Entities
{
    public class User
    {
        public int ID { get; set; }

        [MaxLength(50, ErrorMessage ="Max lenght is 50 syblos")]
        [Required]
        public string Login { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max lenght is 50 characters")]
        [MinLength(4, ErrorMessage = "Min lenght is 4 characters")]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public bool IsBlock { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

    public enum Role
    {
        Client, Manager, Admin
    }
}
