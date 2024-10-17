using Microsoft.AspNetCore.Http.HttpResults;
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
    public class TestRespositary : ITest
    {

        private readonly ShopDbContext _dbContexts;

        public TestRespositary(ShopDbContext dbContexts)
        {
            _dbContexts = dbContexts;   
        }

        public Task<Test> create(Test entity)
        {
           var addEntity = _dbContexts.Add(entity);
           _dbContexts.SaveChanges();
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Test>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Test> getByid(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Test edit)
        {
            throw new NotImplementedException();
        }
    }
}
