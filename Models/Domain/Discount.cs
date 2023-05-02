using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EmployeeClient.Models.Domain
{
    public class Discount
    {
        public int DiscountId { get; set; }
        [Display(Name = "Discount Name")]
        [Required(ErrorMessage = "Discount Name is Required!")]
        public string DiscountName { get; set; } = string.Empty;
        [Display(Name = "Discount Value (%)")]
        public decimal DiscountValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Expire Date")]
        public DateTime ExpireDate { get; set; } = DateTime.UtcNow.AddDays(2);
    }
}