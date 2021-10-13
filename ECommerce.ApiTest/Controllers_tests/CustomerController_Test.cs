using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Api.Controllers;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Services.Customer;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ECommerce.ApiTest
{
    public class CustomerController_Test
    {
        private readonly GetCustomerDTO _getCustomerDTO;
        private readonly Customer _customer;
        private readonly CreateCustomerDTO _createCustomerDTO;
        public CustomerController_Test()
        {
            _customer = new Customer();
            _createCustomerDTO = new CreateCustomerDTO() { Name = "abc", LastName = "klm", Email = "abc@klm.com", Password = "123456" };
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

            var controller = new CustomersController(customerServiceMock.Object);
            //Act

            controller.Add(_createCustomerDTO);

            //Assert

            Assert.Contains<Customer>(_customer, list);
            Assert.Equal(list.Count, beforeAdd + 1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void Update_Customer_By_Id_Test(int identity)
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var list = GetCustomers();
            customerServiceMock.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => CustomerDto(id));

            customerServiceMock.Setup(c => c.Update(It.IsAny<int>(), It.IsAny<CreateCustomerDTO>())).Callback((int id, CreateCustomerDTO customerDto) =>
            {
                var customer = list.FirstOrDefault(c => c.Id == id);
                if (customer == null)
                    throw new Exception("Not Found");
                list.Remove(customer);
                _customer.Id = id;
                _customer.Name = customerDto.Name;
                _customer.LastName = customerDto.LastName;
                _customer.Email = customerDto.Email;
                _customer.Password = customerDto.Password;
                _customer.Orders = new List<Order>();
                list.Add(_customer);
            });
            var controller = new CustomersController(customerServiceMock.Object);

            //Act
            var result=controller.Update(identity, _createCustomerDTO);

            //Assert

            Assert.Equal(list.FirstOrDefault(c => c.Id == identity).LastName, _customer.LastName);
            Assert.Contains<Customer>(_customer, list);
            if (identity > 5)
                Assert.IsType<Exception>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void Delete_Customer_By_Id_Test(int identity)
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var list = GetCustomers();
            int beforeDeleteSize = list.Count;
            customerServiceMock.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => CustomerDto(id));

            customerServiceMock.Setup(c => c.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var customer = list.FirstOrDefault(c => c.Id == id);
                if (customer == null)
                    throw new Exception("Not Found");
                list.Remove(customer);
            });

            var controller = new CustomersController(customerServiceMock.Object);

            //Act
            var result = controller.Delete(identity);
            int afterDeleteSize = list.Count;
            //Assert

            Assert.True(beforeDeleteSize > afterDeleteSize);
            Assert.DoesNotContain<Customer>(list.FirstOrDefault(c => c.Id == identity), list);
            if (identity > 5)
                Assert.IsType<Exception>(result);
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Get_Customer_By_Id_Test(int i)
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => CustomerDto(id));
            var controller = new CustomersController(customerServiceMock.Object);


            //Act
            var result = controller.GetById(i) as OkObjectResult;
            var customer = CustomerDto(i);
            GetCustomerDTO data = result.Value as GetCustomerDTO;
            //Assert
            Assert.Equal(customer.Email, data.Email);
            Assert.Equal(customer.Name, data.Name);
            Assert.Equal(customer.LastName, data.LastName);
            Assert.Equal(customer.GetType(), data.GetType());
        }
        [Fact]
        public void GetAll_Return_Customers()
        {
            //Arrange
            var mockRepo = new Mock<ICustomerService>();
            var list = GetCustomerDtos();
            mockRepo.Setup(c => c.GetAll()).Returns(list);
            var controller = new CustomersController(mockRepo.Object);
            //Act
            var result = controller.GetAll() as OkObjectResult;
            //Assert
            Assert.IsType<OkObjectResult>(result);

            List<GetCustomerDTO> data = result.Value as List<GetCustomerDTO>;
            Assert.NotEmpty(data);
            Assert.Equal(list.Count, data.Count);
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
            _getCustomerDTO.Name = list.FirstOrDefault(c => c.Id == id).Name;
            _getCustomerDTO.LastName = list.FirstOrDefault(c => c.Id == id).LastName;
            _getCustomerDTO.Email = list.FirstOrDefault(c => c.Id == id).Email;

            return _getCustomerDTO;
        }
    }
}
