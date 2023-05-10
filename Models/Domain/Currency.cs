using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        [Display(Name = "Currency Name")]
        public string CurrencyName { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 3)")]
        [Display(Name = "Exchange Rate")]
        public decimal ExchangeRate { get; set; }
        [Display(Name = "Country Name")]
        public string CountryName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
