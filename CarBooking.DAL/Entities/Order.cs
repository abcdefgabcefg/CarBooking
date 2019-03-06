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
        [Display(Name ="Start Date: ")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = requireMes)]
        [Display(Name ="Finish Date: ")]
        public DateTime FinishDate { get; set; }

        [Required(ErrorMessage = requireMes)]
        [Column(TypeName ="char")]
        [StringLength(9, ErrorMessage ="Leng's is 9 characters")]
        [Display(Name ="Passport Number: ")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage =requireMes)]
        [Display(Name ="Need Driver: ")]
        public bool NeedDriver { get; set; }

        [Required(ErrorMessage =requireMes)]
        public Status Status { get; set; }

        [Required(ErrorMessage =requireMes)]
        public int CarID { get; set; }

        [Required(ErrorMessage =requireMes)]
        public int ClientID { get; set; }
        
        public virtual Car Car { get; set; }
        public virtual User Client { get; set; }

        public decimal Price { get; set; }

        [MaxLength(500)]
        [Display(Name ="Manager Comment: ")]
        public string ManagerComment { get; set; }

        [Display(Name ="Repair Price: ")]
        public decimal? RepairPrice { get; set; }

        private const string requireMes = "Required field";
    }

    public enum Status
    {
        NotConfirmed, Confirmed, Paid, Refused, Finished, WPFR
    }
}
