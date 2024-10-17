 using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBusinessLayer.DTO.Brand
{
    public class CreateBrandDto
    {
       
        public string Name { get; set; }

        public int EstablishedYear { get; set; }
    }

    public class createBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {

        public createBrandDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.EstablishedYear).InclusiveBetween(1920, DateTime.UtcNow.Year);
        }

    }

   
}
