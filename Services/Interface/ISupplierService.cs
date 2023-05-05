using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
	public interface ISupplierService
	{
		Supplier CreateSupplier(Supplier supplier);
		Supplier UpdateSupplier(Supplier supplier);
		bool DeleteSupplier(int id);
		Supplier GetSupplierById(int id);
		List<Supplier> GetAllSuppliers();
		string GenerateSupplierCode();
	}
}
