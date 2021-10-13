using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Services.Customer;

namespace ECommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateCustomerDTO customer)
        {
            _customerService.Add(customer);
            return Ok(new { status = true, errors = "" });
        }

        [HttpPut]
        public IActionResult Update([FromQuery] int id,[FromBody] CreateCustomerDTO customer)
        {
            _customerService.Update(id,customer);
            return Ok(new { status = true, errors = "" });
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            _customerService.Delete(id);
            return Ok(new { status = true, errors = "" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var getAll = _customerService.GetAll();
            //return Ok(new { status = true, data = getAll, errors = "" });
           return Ok(getAll);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var getById =_customerService.GetById(id);
            //return Ok(new { status = true, data = getById, errors = "" });
            return Ok(getById);
        }

        [HttpPut("{id}")]
        public IActionResult Delete(int id,[FromBody] CreateCustomerDTO customer)
        {
            _customerService.Update(id,customer);
            //return Ok(new { status = true, errors = "" });
            return Ok();
        }
    }
}
