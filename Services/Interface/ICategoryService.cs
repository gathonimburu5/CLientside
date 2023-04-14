using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        bool DeleteCategory(int id);
        Category GetCategoryById(int id);
        List<Category> GetAllCategories();
    }
}
