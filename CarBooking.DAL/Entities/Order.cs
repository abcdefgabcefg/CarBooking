using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarBooking.DAL.Entities
{
    public class Order
    {
        public int ID { get; set; }

        [Required(ErrorMessage = requireMes)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = requireMes)]
        public DateTime FinishDate { get; set; }

        [Required(ErrorMessage = requireMes)]
        [MaxLength(9, ErrorMessage ="Require 9 numbers")]
        [MinLength(9, ErrorMessage ="Require 9 numbers")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage =requireMes)]
        public bool NeedDriver { get; set; }

        [Required(ErrorMessage =requireMes)]
        public Status Status { get; set; }

        [Required(ErrorMessage =requireMes)]
        public int CarID { get; set; }

        [Required(ErrorMessage =requireMes)]
        //[ForeignKey("User")]
        public int ClientID { get; set; }

        //[ForeignKey("User")]
        public int? ManagerID { get; set; }

        public virtual Car Car { get; set; }
        public virtual User Client { get; set; }
        public virtual User Manager { get; set; }

        public int Price
        {
            get
            {
                if (Car != null)
                {
                    decimal price = Car.Price;
                    if (NeedDriver)
                        price += 20;
                    return Convert.ToInt32(price * Convert.ToDecimal((FinishDate - StartDate).TotalHours));
                }
                return 0;
            }
        }

        [MaxLength(500)]
        public string ManagerComment { get; set; }

        public decimal RepairPrice { get; set; }

        private const string requireMes = "Required field";
    }

    public enum Status
    {
        NotConfirmed, Confirmed, Paid, Refused, Finished, WPFR
    }
}
