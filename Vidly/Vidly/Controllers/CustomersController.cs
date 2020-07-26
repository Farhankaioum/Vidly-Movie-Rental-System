using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MovieRentDbContext _context;

        public CustomersController(MovieRentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var model = new CreateCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(model.Customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            model.MembershipTypes = _context.MembershipTypes.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var viewModel = new CreateCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateCustomerViewModel model)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == model.Customer.Id);
            
            
                if (ModelState.IsValid)
                {
                    customer.Name = model.Customer.Name;
                    customer.BirthDate = model.Customer.BirthDate;
                    customer.IsSubscribedToNewsLetter = model.Customer.IsSubscribedToNewsLetter;
                    customer.MembershipTypeId = model.Customer.MembershipTypeId;

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            model.MembershipTypes = _context.MembershipTypes.ToList();
            return View(model);
        }

        public IActionResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customers);
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


    }
}