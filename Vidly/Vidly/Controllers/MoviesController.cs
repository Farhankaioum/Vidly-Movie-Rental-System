using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
       
        public IActionResult Index()
        {
            var movies = new List<Movie> { 
                new Movie {Id = 101, Name= "Shrek"},
                new Movie {Id = 102, Name= "Wall-e"}
            };
            return View(movies);
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