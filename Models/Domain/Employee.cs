using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";
        [Required]
        public string County { get; set; } = "";
    }
}
