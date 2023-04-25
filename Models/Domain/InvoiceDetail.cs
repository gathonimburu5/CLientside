using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
	public class InvoiceDetail
	{
		public int InvoiceDetailId { get; set; }
		public int Qty { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal SubTotal { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product? Product { get; set; }
		[ForeignKey("Invoice")]
		public int InvoiceId { get; set; }
		public virtual Invoice? Invoice { get; set; }
		public decimal VAT { get; set; }
		[NotMapped]
		public bool IsDeleted { get; set; } = false;
	}
}
