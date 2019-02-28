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
        
        [Required(ErrorMessage =requireMes)]
        [MaxLength(60, ErrorMessage ="Max lenght is 60 characters")]
        public string CompanyTitle { get; set; }

        [Required(ErrorMessage = requireMes)]
        public bool IsLuxury { get; set; }

        [Required(ErrorMessage = requireMes)]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = requireMes)]
        [MaxLength(60, ErrorMessage = "Max lenght is 60 characters")]
        public string CarTitle { get; set; }

        [Required(ErrorMessage =requireMes)]
        public bool IsFree { get; set; }

        private int count;
        private const string requireMes = "Required field";
    }
}
