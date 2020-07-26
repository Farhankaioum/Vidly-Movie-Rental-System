using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MovieRentDbContext _context;

        public CustomersController(MovieRentDbContext context)
        {
            _context = context;
        }

        // Get /api/customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // Get /api/customers/1
        [HttpGet("{id}")]
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new Exception();

            return customer;
        }

        // Post /api/customers
        [HttpPost()]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new Exception();

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // Post /api/customer/1
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new Exception();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new Exception();

            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipType = customer.MembershipType;

            _context.SaveChanges();
        }

        // Delete /api/customer/1
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new Exception();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
