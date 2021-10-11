using ECommerce.Application.DTOs.CustomerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Repositories;

namespace ECommerce.Application.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public void Add(CreateCustomerDTO customerDTO)
        {
            var enterCustomer = _mapper.Map<Domain.Entities.Customer>(customerDTO);
            var customer =  _customerRepository.Get(c => c.Name.Equals(enterCustomer.Name)&&c.LastName.Equals(enterCustomer.LastName)&&c.Email.Equals(enterCustomer.Email));
            if (customer.Equals(null))
            {
                _customerRepository.Add(_mapper.Map<Domain.Entities.Customer>(customerDTO));
            }
            throw new Exception("Already exist");
            
        }

        public void Delete(int id)
        {
            var customer = _customerRepository.Get(c => c.Id == id);
            if(customer.Equals(null))
            {
                throw new Exception("Not exist");
            }
            _customerRepository.Delete(customer);
        }

        public GetCustomerDTO GetById(int id)
        {
            var customer =_customerRepository.Get(c => c.Id == id);
            return _mapper.Map<GetCustomerDTO>(customer);
        }

        public List<GetCustomerDTO> GetAll()
        {
            var customerList = _customerRepository.GetAll();
            return _mapper.Map<List<GetCustomerDTO>>(customerList);
        }

        public void Update(int id, CreateCustomerDTO customerDTO)
        {
            var customer = _customerRepository.Get(c => c.Id == id);
            if (customer.Equals(null))
            {
                throw new Exception("Not exist");
            }
            var changed = _mapper.Map<Domain.Entities.Customer>(customerDTO);
            customer.Name = changed.Name;
            customer.LastName = changed.LastName;
            customer.Email = changed.Email;
            customer.Password = changed.Password;

            _customerRepository.Update(customer);
        }
    }
}
