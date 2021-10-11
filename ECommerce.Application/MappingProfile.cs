using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.BrandDTOs;
using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.DTOs.ProductDTOs;

namespace ECommerce.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerDTO, Domain.Entities.Customer>().ReverseMap();
            CreateMap<GetCustomerDTO, Domain.Entities.Customer>().ReverseMap();

            CreateMap<CategoryDTO, Domain.Entities.Category>().ReverseMap();
            CreateMap<BrandDTO, Domain.Entities.Brand>().ReverseMap();
            CreateMap<ProductDTO, Domain.Entities.Product>().ReverseMap();


        }
    }
}
