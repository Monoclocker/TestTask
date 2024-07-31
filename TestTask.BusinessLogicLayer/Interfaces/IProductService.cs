using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.BusinessLogicLayer.DTOs;

namespace TestTask.BusinessLogicLayer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductJoinDTO>> GetProducts(int page);
        Task<ProductJoinDTO> GetProduct(int id);
        Task CreateProduct(ProductEditDTO productDTO);
        Task UpdateProduct(int id, ProductEditDTO productDTO);
        Task DeleteProduct(int id);
    }
}
