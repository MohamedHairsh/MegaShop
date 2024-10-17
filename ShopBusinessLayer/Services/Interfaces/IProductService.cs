using ShopBusinessLayer.DTO.Product;
using ShopBusinessLayer.InputModels;
using ShopBusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusinessLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);

        Task<PaginationVM<ProductDto>> GetPagination(PaginationIPM pagination);
        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<IEnumerable<ProductDto>> GetAllByFilterAsync(int? categoryId,int? brandId);
        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);

        Task UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(int id);
    }
}
