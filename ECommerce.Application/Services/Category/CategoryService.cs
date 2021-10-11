using ECommerce.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Repositories;

namespace ECommerce.Application.Services.Category
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

        public void Add(CategoryDTO categoryDTO)
        {
            var entered = _mapper.Map<Domain.Entities.Category>(categoryDTO);
            var category = _categoryRepository.Get(c => c.CategoryName == entered.CategoryName );
            if (category.Equals(null))
            {
                _categoryRepository.Add(_mapper.Map<Domain.Entities.Category>(categoryDTO));
            }
            throw new Exception("Already exist");
        }

        public void Delete(int id)
        {
            var category = _categoryRepository.Get(c => c.Id == id);
            if (category.Equals(null))
            {
                throw new Exception("Not exist");
            }
             _categoryRepository.Delete(category);
        }

        public List<CategoryDTO> GetAll()
        {
            var categoryList = _categoryRepository.GetAll();
            return _mapper.Map<List<CategoryDTO>>(categoryList);
        }

        public CategoryDTO GetById(int id)
        {
            var category =_categoryRepository.Get(c => c.Id == id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public void Update(int id, CategoryDTO categoryDTO)
        {
            var category = _categoryRepository.Get(c => c.Id == id);
            if (category.Equals(null))
            {
                throw new Exception("Not exist");
            }
            var changed = _mapper.Map<Domain.Entities.Category>(categoryDTO);
            category.CategoryName = changed.CategoryName;

            _categoryRepository.Update(category);
        }
    }
}
