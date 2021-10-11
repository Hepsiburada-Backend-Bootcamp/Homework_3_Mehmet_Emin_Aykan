using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.ProductDTOs;

namespace ECommerce.Application.Services.Product
{
    public interface IProductService
    {
        void Add(ProductDTO productDTO);

        void Delete(int id);

        void Update(int id, ProductDTO productDTO);

        List<ProductDTO> GetAll();

        ProductDTO GetById(int id);
    }
}
