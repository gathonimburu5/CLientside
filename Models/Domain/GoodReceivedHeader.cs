using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
    public class GoodReceivedHeader
    {
        public int GoodReceivedHeaderId { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public DateTime DateReceived { get; set; } = DateTime.Now;
        [ForeignKey("PInvoiceHeader")]
        public int PInvoiceHeaderId { get; set; }
        public virtual PInvoiceHeader? PInvoiceHeader { get; set; }
        public string AdditionalDetails { get; set; } = string.Empty;
        public virtual List<GoodReceivedDetail> GoodReceivedDetails { get; set; } = new List<GoodReceivedDetail>();
    }
}
