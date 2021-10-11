using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.BrandDTOs;
using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.Services.Brand;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public ProductsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] BrandDTO brand)
        {
            _brandService.Add(brand);
            return Ok(new { status = true, errors = "" });
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            _brandService.Delete(id);
            return Ok(new { status = true, errors = "" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var getAll = _brandService.GetAll();
            return Ok(new { status = true, data = getAll, errors = "" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var getById = _brandService.GetById(id);
            return Ok(new { status = true, data = getById, errors = "" });
        }

        [HttpPut("{id}")]
        public IActionResult Delete(int id, [FromBody] BrandDTO brand)
        {
            _brandService.Update(id, brand);
            return Ok(new { status = true, errors = "" });
        }
    }
}
