using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name Required")]
        public string ProductName { get; set; } = string.Empty;
        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Product Description Required")]
        public string ProductDescription { get; set; } = string.Empty;
        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "Product Code Required")]
        public string ProductCode { get; set; } = string.Empty;
        [Display(Name = "Product Quantity")]
        [Required(ErrorMessage = "Product Quantity Required")]
        public int ProductQty { get; set; }
        [Display(Name = "Product Buying Price")]
        [Required(ErrorMessage = "Product Buying Price Required")]
        public decimal ProductBuyingPrice { get; set; }
        [Display(Name = "Product Selling Price")]
        [Required(ErrorMessage = "Product Selling Price Required")]
        public decimal ProductSellingPrice { get; set; }
        [Display(Name = "Product Reorder Level")]
        [Required(ErrorMessage = "Product Reorder Level Required")]
        public int ProductReorderLevel { get; set; }
        [Display(Name = "Product VAT(%)")]
        [Required(ErrorMessage = "Product VAT Required")]
        public int ProductVat { get; set; }
        [Display(Name = "Product Supplier")]
        [Required(ErrorMessage = "Product Supplier Required")]
        public string ProductSupplier { get; set; } = string.Empty;
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category Required")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        [ForeignKey("MeasureUnit")]
        [Display(Name = "Measure Unit")]
        [Required(ErrorMessage = "Measure Unit Required")]
        public int MeasureUnitId { get; set; }
        public virtual MeasureUnit? MeasureUnit { get; set; }
        public string? ProductImage { get; set; }
        [NotMapped]
        public IFormFile? ProductPhoto { get; set; }
    }
}
