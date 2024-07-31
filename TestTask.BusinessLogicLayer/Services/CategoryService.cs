using TestTask.BusinessLogicLayer.DTOs;
using TestTask.BusinessLogicLayer.Exceptions;
using TestTask.BusinessLogicLayer.Interfaces;
using TestTask.DataAccessLayer.Entities;
using TestTask.DataAccessLayer.Interfaces;

namespace TestTask.BusinessLogicLayer.Services
{
    public class CategoryService: ICategoryService
    {
        IUnitOfWork Context;
        public CategoryService(IUnitOfWork uow) 
        {
            Context = uow;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return await Task.FromResult(Context
                .Categories
                .GetAll()
                .Select(obj => new CategoryDTO
            {
                Id = obj.Id,
                Name = obj.Name
            }));
        }

        public async Task CreateCategory(CategoryEditDTO category)
        {
            Context.Categories.Create(
                new Category()
                {
                    Name = category.Name,
                }
            );

            await Task.CompletedTask;
        }

        public async Task UpdateCategory(int id, CategoryEditDTO category) 
        {
            Category? categoryForUpdate = Context.Categories.Get(id);

            if (categoryForUpdate is null) 
                await Task.FromException(new UnknownEntityException("Unknown Category"));

            categoryForUpdate!.Name = category.Name;

            Context.Categories.Update(categoryForUpdate);

            await Task.CompletedTask;
        }

        public async Task DeleteCategory(int id) 
        {
            Context.Categories.Delete(id);

            await Task.CompletedTask;
        }
    }
}
