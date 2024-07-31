using TestTask.BusinessLogicLayer.DTOs;

namespace TestTask.BusinessLogicLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task CreateCategory(CategoryEditDTO dto);
        Task UpdateCategory(int id, CategoryEditDTO dto);
        Task DeleteCategory(int id);
    }
}
