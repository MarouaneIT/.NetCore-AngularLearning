using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task<IReadOnlyList<Product>> GetAllProducts();
    }
}
