using System;
using System.Collections.Generic;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using Moq;
using Xunit;

namespace ECommerce.InfrastructureTest.Repositories_Tests
{
    public class CustomerRepository_Test
    {
        

        [Fact]
        public void GetAll_Return_Customers()
        {
            //Arrange

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var list = Customers();
            customerRepositoryMock.Setup(repo => repo.GetAll()).Returns(list);
            
            ICustomerRepository customerRepository = customerRepositoryMock.Object;
            
            //Act

            var result = customerRepository.GetAll();
            
            //Assert
            
            Assert.NotNull(result);
            Assert.Equal(list.Count, result.Count);
            Assert.NotEmpty(result);

        }

        private List<Customer> Customers()
        {
            List<Customer> customers = new();
            for (int i = 0; i < 5; i++)
            {
                Customer customer = new();
                customer.Id = i + 1;
                customer.Name = $"{i} Name";
                customer.LastName = $"{i+5} LastName";
                customer.Email = $"{i}_{i + 5}@gmail.com";
                customer.Orders = new List<Order>();
                customer.Password = $"{i}{i + 1}{i + 2}";

                customers.Add(customer);
            }

            return customers;
        }
    }
}
