using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieRentDbContext _context;

        public MoviesController(MovieRentDbContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);

            return View(movie);
        }

        public IActionResult Random(int id)
        {
            Movie movie = new Movie
            {
                Id = 101,
                Name = "Shrek!"
            };
            var customers = new List<Customer>
            {
                new Customer{Name = "Customer 1"},
                new Customer{Name = "Customer 2"}
            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

    }
}