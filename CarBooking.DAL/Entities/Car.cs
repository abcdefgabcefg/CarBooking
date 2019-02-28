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


        private const string requireMes = "Required field";
    }

    class CarEqualityComparer : IEqualityComparer<Car>
    {
        public bool Equals(Car x, Car y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else
            {
                return x.CarTitle == y.CarTitle;
            }
        }

        public int GetHashCode(Car obj)
        {
            return obj.ID;
        }
    }
}
