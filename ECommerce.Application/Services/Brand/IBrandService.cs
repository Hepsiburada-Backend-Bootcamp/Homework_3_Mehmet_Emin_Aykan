using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.BrandDTOs;

namespace ECommerce.Application.Services.Brand
{
    public interface IBrandService
    {
        void Add(BrandDTO brandDTO);

        void Delete(int id);

        void Update(int id, BrandDTO brandDTO);

        List<BrandDTO> GetAll();

        BrandDTO GetById(int id);
    }
}
