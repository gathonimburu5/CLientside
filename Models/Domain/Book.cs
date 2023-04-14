using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title Required.")]
        public string Title { get; set; } = "";
        [Required(ErrorMessage = "Genre Required.")]
        public string Genre { get; set; } = "";
        [Required(ErrorMessage = "Publisher Required.")]
        public string Publisher { get; set; } = "";
    }
}
