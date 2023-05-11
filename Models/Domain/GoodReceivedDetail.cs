using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
    public class GoodReceivedDetail
    {
        public int GoodReceivedDetailId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Qty { get; set; }
        [ForeignKey("GetGoodReceivedHeader")]
        public int GoodReceivedHeaderId { get; set; }
        public virtual GoodReceivedHeader? GetGoodReceivedHeader { get; set; }
    }
}
