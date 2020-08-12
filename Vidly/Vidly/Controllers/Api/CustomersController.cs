using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Dto;
using Vidly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MovieRentDbContext _context;

        public IMapper Mapper { get; }

        public CustomersController(MovieRentDbContext context, IMapper mapper)
        {
            _context = context;
            Mapper = mapper;
        }

        // Get /api/customers
        [HttpGet]
        public IActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType).AsQueryable();

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        // Get /api/customers/1
        [HttpGet("{id}")]
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new Exception();

            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // Post /api/customers
        [HttpPost()]
        public ActionResult<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created("", customerDto);
        }

        // Post /api/customer/1
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new Exception();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new Exception();

            // If you have existing object than pass it second argument.
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
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
