using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface ICurrencyService
    {
        Currency CreateCurrency(Currency currency);
        Currency UpdateCurrency(Currency currency);
        bool DeleteCurrency(int id);
        Currency GetCurrencyById(int id);
        List<Currency> GetAllCurrency();
    }
}
