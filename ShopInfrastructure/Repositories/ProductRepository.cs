using Microsoft.EntityFrameworkCore;
using ShopDomainLayer.Contracts;
using ShopDomainLayer.Models;
using ShopInfrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopInfrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ShopDbContext dbContext):base(dbContext)
        {
                
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _dbContext.product.Include(x=>x.Category).Include(x=>x.Brand).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetDetailsAsync(int id)
        {
            return await _dbContext.product.Include(x => x.Category).Include(x => x.Brand).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
           _dbContext.Update(product);
           await _dbContext.SaveChangesAsync();
        }
    }
}
