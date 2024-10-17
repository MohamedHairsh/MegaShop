using AutoMapper;
using ShopBusinessLayer.DTO.Category;
using ShopBusinessLayer.Services.Interfaces;
using ShopDomainLayer.Contracts;
using ShopDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusinessLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
        }
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var createEntity = await _categoryRepository.CreateAsync(category);
            var entity = _mapper.Map<CategoryDto>(createEntity);
            return entity;  
            
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(x => x.Id == id);
            await _categoryRepository.DeleteAsync(category);
        }
        

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(category);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(x=>x.Id==id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
             await _categoryRepository.UpdateAsync(category);
        }
    }
}
