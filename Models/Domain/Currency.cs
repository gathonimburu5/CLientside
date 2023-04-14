using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeClient.Models.Domain
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ExchangeRate { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
}
