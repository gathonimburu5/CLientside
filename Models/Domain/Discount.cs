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
		[Display(Name = "Discount Value")]
		public decimal DiscountValue { get; set; }
		[Display(Name = "Start Date")]
		public DateTimeOffset StartDate { get; set; } = DateTimeOffset.UtcNow;
		[Display(Name = "Expire Date")]
		public DateTimeOffset ExpireDate { get; set; } = DateTimeOffset.UtcNow.AddDays(2);
	}
}