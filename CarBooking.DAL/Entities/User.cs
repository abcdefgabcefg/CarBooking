using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace CarBooking.DAL.Entities
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Max length is 50 characters")]
        [Index(IsUnique =true)]
        public string Login { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Max lenght is 50 characters")]
        public string Password { get; set; }

        public Role Role { get; set; }

        public bool IsBlock { get; set; }
    }

    public enum Role
    {
        Client, Manager, Admin
    }
}
