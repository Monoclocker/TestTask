using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.BusinessLogicLayer.DTOs;
using TestTask.BusinessLogicLayer.Exceptions;
using TestTask.BusinessLogicLayer.Interfaces;
using TestTask.DataAccessLayer.Entities;
using TestTask.DataAccessLayer.Interfaces;

namespace TestTask.BusinessLogicLayer.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork Context;

        public ProductService(IUnitOfWork uow)
        {
            Context = uow;
        }

        public async Task<IEnumerable<ProductJoinDTO>> GetProducts(int page)
        {
            IEnumerable<ProductJoinDTO> join = Context.Products
                .GetAll()
                .Join(
                    Context.Categories.GetAll(),
                    p => p.CategoryId,
                    c => c.Id,
                    (p, c) => new ProductJoinDTO()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryName = c.Name
                    }
                )
                .Skip(15 * (page - 1))
                .Take(15);

            return await Task.FromResult(join);
        }

        public async Task<ProductJoinDTO> GetProduct(int id)
        {
            ProductJoinDTO? join = Context.Products.GetAll()
                .Where(x => x.Id == id)
                .Join(
                    Context.Categories.GetAll(),
                    p => p.CategoryId,
                    c => c.Id,
                    (p, c) => new ProductJoinDTO()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryName = c.Name
                    }
                )
                .FirstOrDefault();

            if (join == null)
            {
                await Task.FromException(new UnknownEntityException("Unknown Product"));
            }

            return await Task.FromResult(join!);
        }

        public async Task CreateProduct(ProductEditDTO productDTO)
        {
            Context.Products.Create(new Product()
            {
                Name = productDTO.Name,
                CategoryId = productDTO.CategoryId,
                Price = productDTO.Price
            });

            await Task.CompletedTask;
        }

        public async Task UpdateProduct(int id, ProductEditDTO productDTO)
        {
            Product? productForUpdate = Context.Products.Get(id);

            if (productForUpdate is null)
                await Task.FromException(new UnknownEntityException("Unknown Product"));

            productForUpdate!.Name = productDTO.Name;
            productForUpdate!.CategoryId = productDTO.CategoryId;
            productForUpdate!.Price = productDTO.Price;

            Context.Products.Update(productForUpdate);

            await Task.CompletedTask;
        }

        public async Task DeleteProduct(int id)
        {
            Context.Products.Delete(id);
            await Task.CompletedTask;
        }
    }
}
