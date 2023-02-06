using Core.Entities;
using Core.Specifications;
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

        Task<IReadOnlyList<ProductBrand>> GetAllBrands();

        Task<IReadOnlyList<ProductType>> GetAllProductTypes();
        
        
    }
}
