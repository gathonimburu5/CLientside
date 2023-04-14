using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee CreateEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        Employee GetEmployeeById(Guid id);
        void DeleteEmployee(Guid id);
    }
}
