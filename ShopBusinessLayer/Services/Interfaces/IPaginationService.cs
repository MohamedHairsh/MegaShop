using ShopBusinessLayer.InputModels;
using ShopBusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusinessLayer.Services.Interfaces
{
    public interface IPaginationService<T,S> where T:class
    {
        PaginationVM<T> GetPagination(List<S> source,PaginationIPM pagination);
    }
}
