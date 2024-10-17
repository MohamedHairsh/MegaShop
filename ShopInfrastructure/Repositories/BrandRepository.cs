using ShopDomainLayer.Contracts;
using ShopDomainLayer.Models;
using ShopInfrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopInfrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>,IBrandRepository
    {

        public BrandRepository(ShopDbContext dbContext) : base(dbContext) 
        {
                
        }

        public async Task UpdateAsync(Brand brand)
        {
             _dbContext.Update(brand);
            await _dbContext.SaveChangesAsync();
        }
    }
}
