using ShopDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomainLayer.Contracts
{
    public interface ITest
    {
        Task<Test>  create(Test entity);
        Task<List<Test>> GetAll();

        Task<Test> getByid(int id);
        Task Update(Test edit);
        Task Delete(int id);

    }
}
