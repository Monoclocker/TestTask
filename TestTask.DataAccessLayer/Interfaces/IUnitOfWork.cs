using TestTask.DataAccessLayer.Entities;

namespace TestTask.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Category> Categories { get; }
        public IRepository<Product> Products { get; }
    }
}
