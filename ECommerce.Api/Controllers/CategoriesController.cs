using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Services.Category;
using ECommerce.Application.Services.Customer;

namespace ECommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryDTO category)
        {
             _categoryService.Add(category);
            return Ok(new { status = true, errors = "" });
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok(new { status = true, errors = "" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var getAll = _categoryService.GetAll();
            return Ok(new { status = true, data = getAll, errors = "" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var getById =_categoryService.GetById(id);
            return Ok(new { status = true, data = getById, errors = "" });
        }

        [HttpPut("{id}")]
        public IActionResult Delete(int id, [FromBody] CategoryDTO category)
        {
            _categoryService.Update(id, category);
            return Ok(new { status = true, errors = "" });
        }
    }
}
