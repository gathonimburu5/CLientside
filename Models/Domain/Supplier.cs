using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
	public class Supplier
	{
		public int SupplierId { get; set; }
		[Display(Name = "Supplier Name")]
		[Required(ErrorMessage = "Supplier Name Required?")]
		public string SupplierName { get; set; } = string.Empty;
		[Display(Name = "Supplier Code")]
		[Required(ErrorMessage = "Supplier Code Required?")]
		public string SupplierCode { get; set; } = string.Empty;
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Supplier Email")]
		[Required(ErrorMessage = "Supplier Email Required?")]
		public string SupplierEmail { get; set; } = string.Empty;
		[Display(Name = "Bank Name")]
		[Required(ErrorMessage = "Bank Name Required?")]
		public string BankName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Branch is Required?")]
		public string Branch { get; set; } = string.Empty;
		[Display(Name = "Account Number")]
		[Required(ErrorMessage = "Account Number Required?")]
		public string AccountNumber { get; set; } = string.Empty;
		[Display(Name = "Postal Address")]
		[Required(ErrorMessage = "Postal Address Required?")]
		public string PostalAddress { get; set; } = string.Empty;
		[Display(Name = "Physical Address")]
		[Required(ErrorMessage = "Physical Address Required?")]
		public string PhysicalAddress { get; set; } = string.Empty;
		[ForeignKey("Currency")]
		[Display(Name = "Currency")]
		public int CurrencyId { get; set; }
		public virtual Currency? Currency { get; set; }
		[Display(Name = "KRA PIN")]
		[Required(ErrorMessage = "KRA PIN Required?")]
		public string KRAPin { get; set; } = string.Empty;
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Phone Number")]
		[Required(ErrorMessage = "Phone Number Required?")]
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
