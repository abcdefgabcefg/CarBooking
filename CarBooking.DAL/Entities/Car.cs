using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarBooking.DAL.Entities
{
    // don't forget to change repository updating logic
    public class Car
    {
        public int ID { get; set; }

        [Required(ErrorMessage = requireMes)]
        public bool IsLuxury { get; set; }

        // in dollars per hour
        [Required(ErrorMessage = requireMes)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = requireMes)]
        [MaxLength(60, ErrorMessage = "Max lenght is 60 characters")]
        public string CarTitle { get; set; }

        [Required(ErrorMessage =requireMes)]
        public bool IsFree { get; set; }

        [Required(ErrorMessage =requireMes)]
        [MaxLength(300)]
        public string ImagePath { get; set; }

        private const string requireMes = "Required field";
    }

}
