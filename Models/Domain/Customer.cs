using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.Models.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhysicalAddress { get; set; } = string.Empty;
        public string VatPin { get; set; } = string.Empty;
        public int CreditLimit { get; set; }
        public int CreditTerms { get; set; }
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }
    }
}
