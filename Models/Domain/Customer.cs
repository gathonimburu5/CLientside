using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.Models.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        [Display(Name = "Physical Address")]
        public string PhysicalAddress { get; set; } = string.Empty;
        [Display(Name = "KRA PIN")]
        public string VatPin { get; set; } = string.Empty;
        [Display(Name = "Credit Limit")]
        public int CreditLimit { get; set; }
        [Display(Name = "Credit Terms")]
        public int CreditTerms { get; set; }
        [ForeignKey("Currency")]
        [Display(Name = "Currency Name")]
        public int CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }
    }
}
