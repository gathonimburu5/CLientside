using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeClient.Models.Domain
{
    public class PInvoiceHeader
    {
        public int PInvoiceHeaderId { get; set; }
        [ForeignKey("Supplier")]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        [Display(Name = "Ref. Code")]
        public string InvoiceCode { get; set; } = string.Empty;
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        [Display(Name = "Invoice Due Date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDue { get; set; } = DateTime.Now.AddMonths(2);
        public decimal Total { get; set; }
        [Display(Name = "Purchase Invoice Description")]
        public string Description { get; set; } = string.Empty;
        public virtual List<PInvoiceDetail> PInvoiceDetails { get; set; } = new List<PInvoiceDetail>();
    }
}