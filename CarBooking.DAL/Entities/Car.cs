using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarBooking.DAL.Entities
{
    public class Car
    {
        public int ID { get; set; }

        [Required(ErrorMessage = requireMes)]
        [Display(Name ="Luxury:")]
        public bool IsLuxury { get; set; }

        // in dollars per hour
        [Required(ErrorMessage = requireMes)]
        [Display(Name ="Price: ")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = requireMes)]
        [MaxLength(60, ErrorMessage = "Max lenght is 60 characters")]
        [Display(Name ="Car: ")]
        public string CarTitle { get; set; }

        [Required(ErrorMessage =requireMes)]
        [Display(Name ="Free: ")]
        public bool IsFree { get; set; }

        [Required(ErrorMessage =requireMes)]
        [MaxLength(300)]
        [Display(Name ="Image Path: ")]
        public string ImagePath { get; set; }

        private const string requireMes = "Required field";
    }

}
