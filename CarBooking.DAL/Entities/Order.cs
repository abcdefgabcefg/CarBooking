using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        public string PassportNumber { get; set; }

        [Required(ErrorMessage =requireMes)]
        public bool NeedDriver { get; set; }

        [Required(ErrorMessage =requireMes)]
        public int CarID { get; set; }

        public virtual Car Car { get; set; }

        private const string requireMes = "Required field";
    }
}
