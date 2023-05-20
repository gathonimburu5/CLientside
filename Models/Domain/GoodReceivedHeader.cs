using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
    public class GoodReceivedHeader
    {
        public int GoodReceivedHeaderId { get; set; }
        [Display(Name = "Received Date")]
        [DataType(DataType.Date)]
        public DateTime DateReceived { get; set; } = DateTime.Now;
        [ForeignKey("PInvoiceHeader")]
        public int PInvoiceHeaderId { get; set; }
        public virtual PInvoiceHeader? PInvoiceHeader { get; set; }
        [Display(Name = "Invoice Code")]
        public string InvoiceCode { get; set; } = string.Empty;
        [Display(Name = "Additional Details")]
        public string AdditionalDetails { get; set; } = string.Empty;
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; } = string.Empty;
        public virtual List<GoodReceivedDetail> GoodReceivedDetails { get; set; } = new List<GoodReceivedDetail>();
    }
}
