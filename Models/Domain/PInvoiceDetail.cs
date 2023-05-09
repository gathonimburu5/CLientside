using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeClient.Models.Domain
{
	public class PInvoiceDetail
	{
		public int PInvoiceDetailId { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product? Product { get; set; }
		public int QTY { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal SubTotal { get; set; }
		public decimal VatAmount { get; set; }
		[ForeignKey("PInvoiceHeader")]
		public int PInvoiceHeaderId { get; set; }
		public virtual PInvoiceHeader? PInvoiceHeader { get; set; }
		public int Vat { get; set; }
		public string ProductCode { get; set; } = string.Empty;
		[NotMapped]
		public bool IsDeleted { get; set; } = false;
	}
}