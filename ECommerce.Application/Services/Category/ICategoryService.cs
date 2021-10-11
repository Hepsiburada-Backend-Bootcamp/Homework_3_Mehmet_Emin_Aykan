using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.DTOs.CustomerDTOs;

namespace ECommerce.Application.Services.Category
{
    public interface ICategoryService
    {
        void Add(CategoryDTO categoryDTO);

        void Delete(int id);

        void Update(int id, CategoryDTO categoryDTO);

        List<CategoryDTO> GetAll();

        CategoryDTO GetById(int id);
    }
}
