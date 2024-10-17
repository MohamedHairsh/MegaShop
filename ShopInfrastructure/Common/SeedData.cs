using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopDomainLayer.Models;
using ShopInfrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopInfrastructure.Common
{
    public class SeedData
    {
        public static  async  Task SeedRoles(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name="ADMIN",NormalizedName="ADMIN"},
                new IdentityRole{Name="CUSTOMER",NormalizedName="CUSTOMER"}
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
        public static async Task SeedDataAsync(ShopDbContext _dbContext)
        {
            if (!_dbContext.brand.Any())
            {
                await _dbContext.AddRangeAsync(

                new Brand { Name = "One-Plus", EstablishedYear = 1953 },
                new Brand { Name = "Samsung",  EstablishedYear = 1954 },
                new Brand { Name = "Hp",       EstablishedYear = 1953 },
                new Brand { Name = "Acer",     EstablishedYear = 1957 },
                new Brand { Name = "Lenovo",   EstablishedYear = 1954 },
                new Brand { Name = "Dell",     EstablishedYear = 1953 },
                new Brand { Name = "Apple",    EstablishedYear = 1956 });



            }
            await _dbContext.SaveChangesAsync();
          

        }
    }
}
