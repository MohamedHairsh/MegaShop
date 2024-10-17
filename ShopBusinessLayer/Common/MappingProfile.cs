using AutoMapper;
using ShopBusinessLayer.DTO.Brand;
using ShopBusinessLayer.DTO.Category;
using ShopBusinessLayer.DTO.Product;
using ShopDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusinessLayer.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap <Brand ,BrandDto>().ReverseMap();
            CreateMap<Brand,CreateBrandDto>().ReverseMap();
            CreateMap<Brand,UpdateBrandDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category,CreateCategoryDto> ().ReverseMap();
            CreateMap<Category,UpdateCategoryDto> ().ReverseMap();

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(source => source.Category.Name))
                .ForMember(x => x.Brand, opt => opt.MapFrom(source => source.Brand.Name));

        }
    }
}
