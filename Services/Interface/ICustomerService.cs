using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface ICustomerService
    {
        Customer CreateCustomer(Customer customer);
        Customer GetCustomerById(int id);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(int id);
        List<Customer> GetAllCustomer();
        string CustomerCode();
    }
}
