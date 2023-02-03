using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

       public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
