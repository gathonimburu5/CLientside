using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.Models.Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is Required!!")]
        public string CategoryName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
