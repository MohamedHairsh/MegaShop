using ShopDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomainLayer.Contracts
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task UpdateAsync(Category category);
    }
}
