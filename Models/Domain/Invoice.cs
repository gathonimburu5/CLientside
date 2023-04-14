using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.Models.Domain
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime InvoiceDate { get; set; } = System.DateTime.Now;
        public decimal Total { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public string InvoiceCode { get; set; } = string.Empty;
        public virtual List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}
