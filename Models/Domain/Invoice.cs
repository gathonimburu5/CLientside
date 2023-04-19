using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NuGet.Packaging.Signing;

namespace EmployeeClient.Models.Domain
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal Total { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public string InvoiceCode { get; set; } = string.Empty;
        public virtual List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}
