using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        List<Customer> customers;
        public CustomersController()
        {
            customers = new List<Customer> {

                new Customer{Id = 1, Name = "John Smith"},
                new Customer{Id = 2, Name = "Marry Williams"}
            };
        }
        public IActionResult Index()
        {
           
            return View(customers);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }
}