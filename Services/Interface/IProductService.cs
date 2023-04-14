using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface IProductService
    {
        Product CreateProduct(Product product);
        Product UpdateProduct(Product product);
        bool DeleteProduct(int id);
        Product GetProductById(int id);
        List<Product> GetAllProducts();
        string ProductCode();
    }
}
