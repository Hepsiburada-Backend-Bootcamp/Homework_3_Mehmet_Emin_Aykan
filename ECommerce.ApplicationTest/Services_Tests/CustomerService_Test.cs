using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Services.Customer;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using Moq;
using Xunit;

namespace ECommerce.ApplicationTest
{
    public class CustomerService_Test
    {
        private readonly GetCustomerDTO _getCustomerDTO;
        private readonly Customer _customer;
        private readonly CreateCustomerDTO _createCustomerDTO;
        public CustomerService_Test()
        {
            _customer =new Customer();
            _createCustomerDTO = new CreateCustomerDTO(){Name="abc",LastName="klm",Email="abc@klm.com",Password="123456"};
            _getCustomerDTO = new GetCustomerDTO();
        }

        [Fact]
        public void Add_Customer_Test()
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var list = GetCustomers();
            int beforeAdd = list.Count;
            customerServiceMock.Setup(c => c.Add(It.IsAny<CreateCustomerDTO>())).Callback(
                (CreateCustomerDTO customerDto) =>
                {
                    _customer.Id = list.Count;
                    _customer.Name = customerDto.Name;
                    _customer.LastName = customerDto.LastName;
                    _customer.Email = customerDto.Email;
                    _customer.Password = customerDto.Password;
                    _customer.Orders = new List<Order>();
                    list.Add(_customer);
                });

            ICustomerService customerService = customerServiceMock.Object;
            //Act

            customerService.Add(_createCustomerDTO);

            //Assert

            Assert.Contains<Customer>(_customer, list);
            Assert.Equal(list.Count, beforeAdd+1);
        }

        [Fact]
        public void Update_Customer_By_Id_Test()
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var list = GetCustomers();
            customerServiceMock.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => CustomerDto(id));

            customerServiceMock.Setup(c => c.Update(It.IsAny<int>(), It.IsAny<CreateCustomerDTO>())).Callback((int id, CreateCustomerDTO customerDto) =>
            {
                var customer = list.FirstOrDefault(c => c.Id == id);
                list.Remove(customer);
                _customer.Id = id;
                _customer.Name = customerDto.Name;
                _customer.LastName = customerDto.LastName;
                _customer.Email = customerDto.Email;
                _customer.Password = customerDto.Password;
                _customer.Orders = new List<Order>();
                list.Add(_customer);
            });
            ICustomerService customerService = customerServiceMock.Object;

            //Act
            customerService.Update(1, _createCustomerDTO);

            //Assert

            Assert.Equal(list.FirstOrDefault(c => c.Id == 1).LastName, _customer.LastName);
            Assert.Contains<Customer>(_customer, list);
        }



        [Fact]
        public void Delete_Customer_By_Id_Test()
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var list = GetCustomers();
            int beforeDeleteSize = list.Count;
            customerServiceMock.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => CustomerDto(id));

            customerServiceMock.Setup(c => c.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var customer = list.FirstOrDefault(c=>c.Id==id);
                list.Remove(customer);
            });

            ICustomerService customerService = customerServiceMock.Object;
            
            //Act
            customerService.Delete(1);

            //Assert
            int afterDeleteSize = list.Count;
            Assert.True(beforeDeleteSize>afterDeleteSize);
            Assert.DoesNotContain<Customer>(list.FirstOrDefault(c => c.Id == 1),list);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Get_Customer_By_Id_Test(int i)
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(c=>c.GetById(It.IsAny<int>())).Returns((int id)=>CustomerDto(id));
            ICustomerService customerService = customerServiceMock.Object;
            
            
            //Act
            var result = customerService.GetById(i);
            var customer=CustomerDto(i);

            //Assert
            Assert.Equal(customer.Email,result.Email);
            Assert.Equal(customer.Name, result.Name);
            Assert.Equal(customer.LastName, result.LastName);
            Assert.Equal(customer.GetType(), result.GetType());
        }


        [Fact]
        public void GetAll_Return_Customers()
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var list = GetCustomerDtos();
            customerServiceMock.Setup(c => c.GetAll()).Returns(list);

            ICustomerService customerRepository = customerServiceMock.Object;

            //Act

            var result = customerRepository.GetAll();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(list.Count, result.Count);
            Assert.NotEmpty(result);

        }

        private List<Customer> GetCustomers()
        {
            List<Customer> customers = new();
            for (int i = 0; i < 5; i++)
            {
                Customer customer = new();
                customer.Id = i + 1;
                customer.Name = $"{i} Name";
                customer.LastName = $"{i + 5} LastName";
                customer.Email = $"{i}_{i + 5}@gmail.com";
                customer.Orders = new List<Order>();
                customer.Password = $"{i}{i + 1}{i + 2}";

                customers.Add(customer);
            }

            return customers;
        }

        private List<GetCustomerDTO> GetCustomerDtos()
        {
            var customers = GetCustomers();
            List<GetCustomerDTO> dtoList = new();
            foreach (var customer in customers)
            {
                _getCustomerDTO.Email = customer.Email;
                _getCustomerDTO.Name = customer.Name;
                _getCustomerDTO.LastName = customer.LastName;
                dtoList.Add(_getCustomerDTO);
            }

            return dtoList;
        }

        private GetCustomerDTO CustomerDto(int id)
        {
            var list = GetCustomers();
            _getCustomerDTO.Name= list.FirstOrDefault(c=>c.Id==id).Name;
            _getCustomerDTO.LastName = list.FirstOrDefault(c => c.Id == id).LastName;
            _getCustomerDTO.Email = list.FirstOrDefault(c => c.Id == id).Email;

            return _getCustomerDTO;
        }
        

    }
}
