using AutoMapper;
using ShopBusinessLayer.InputModels;
using ShopBusinessLayer.Services.Interfaces;
using ShopBusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusinessLayer.Services
{
    public class PaginationService<T, S> : IPaginationService<T, S> where T : class
    {
        private readonly IMapper _mapper;

        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public PaginationVM<T> GetPagination(List<S> source, PaginationIPM pagination)
        {
            var currentPage = pagination.PageNumber;
            var pageSize = pagination.pageSize;
            var totalNoOfRecords = source.Count;
            var totalPages = (int)Math.Ceiling(totalNoOfRecords / (double)pageSize);

            var result = source
                .Skip((pagination.PageNumber - 1) * (pagination.pageSize))
                .Take((pagination.pageSize))
                .ToList();

            var items = _mapper.Map<List<T>>(result);

            PaginationVM<T> paginationVm = new PaginationVM<T>(currentPage,totalPages,pageSize,totalNoOfRecords,items);
            return paginationVm;
        }
    }
}
