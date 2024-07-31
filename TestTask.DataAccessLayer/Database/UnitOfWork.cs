using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.DataAccessLayer.Entities;
using TestTask.DataAccessLayer.Interfaces;

namespace TestTask.DataAccessLayer.Database
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private DbContext context;

        private IRepository<Product>? ProductRepository;
        private IRepository<Category>? CategoryRepository;

        public IRepository<Product> Products { get => ProductRepository ?? new ProductRepository(context); }
        public IRepository<Category> Categories { get => CategoryRepository ?? new CategoryRepository(context); }

        public UnitOfWork(IConfiguration configuration) 
        {
            context = new DbContext(configuration);
        }

        public void Dispose() { context.Dispose(); }
    }
}
