using AutoMapper;
using ShopBusinessLayer.DTO.Brand;
using ShopBusinessLayer.Exceptions;
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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto)
        {
            var validator = new createBrandDtoValidator();
            var validationResult = await validator.ValidateAsync(createBrandDto);
            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Brand Input", validationResult);
            }
            var brand = _mapper.Map<Brand>(createBrandDto);
            var createEntity = await _brandRepository.CreateAsync(brand);
            var entity = _mapper.Map<BrandDto>(createEntity);
            return entity;
        }


        public async Task DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x =>x.Id == id);
            await _brandRepository.DeleteAsync(brand);
        }
        
        

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brand = await _brandRepository.GetAllAsync();
            return _mapper.Map<List<BrandDto>>(brand);
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x=> x.Id == id);
           
            return  _mapper.Map<BrandDto>(brand);
        }

        public async Task UpdateAsync(UpdateBrandDto updateBrandDto)
        {
            var brand = _mapper.Map<Brand>(updateBrandDto);
            await _brandRepository.UpdateAsync(brand);
        }
    }
}
