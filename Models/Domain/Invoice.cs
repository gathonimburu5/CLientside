using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NuGet.Packaging.Signing;

namespace EmployeeClient.Models.Domain
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:2}")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        [Display(Name = "Invoice Date Due")]
        [DisplayFormat(DataFormatString = "{0:2}")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDateDue { get; set; } = DateTime.UtcNow.AddDays(7);
        public decimal Total { get; set; }
        [ForeignKey("Customer")]
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        [Display(Name = "Invoice Code")]
        public string InvoiceCode { get; set; } = string.Empty;
        public virtual List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}
