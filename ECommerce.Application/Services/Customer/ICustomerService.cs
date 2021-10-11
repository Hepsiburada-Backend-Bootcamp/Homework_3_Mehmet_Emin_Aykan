using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.CustomerDTOs;

namespace ECommerce.Application.Services.Customer
{
    public interface ICustomerService
    {
        void Add(CreateCustomerDTO customerDTO);

        void Delete(int id);

        void Update(int id, CreateCustomerDTO customerDTO);

        List<GetCustomerDTO> GetAll();

        GetCustomerDTO GetById(int id);
    }
}
