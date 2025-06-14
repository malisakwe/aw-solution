using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AdventureWorksDbContext _context;
        public ProductRepository(AdventureWorksDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id); 
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync(); 
        }
    }
}
