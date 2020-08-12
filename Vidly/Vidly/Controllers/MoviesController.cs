using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieRentDbContext _context;

        public MoviesController(MovieRentDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "CanManageMovies")]
        [HttpGet]
        public ActionResult Save()
        {
            var genres = _context.Genres.ToList();

            var model = new SaveMovieViewModel()
            {
                Genres = genres
            };
            return View(model);
        }

        [Authorize(Roles = "CanManageMovies")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.Id == 0)
                {
                    movie.DateAdded = DateTime.Now;
                    _context.Movies.Add(movie);
                }
                else
                {
                    var existingMovie = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
                    existingMovie.Name = movie.Name;
                    existingMovie.GenreId = movie.GenreId;
                    existingMovie.ReleaseDate = movie.ReleaseDate;
                    existingMovie.Stock = movie.Stock;
                }

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            var viewModel = new SaveMovieViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize(Roles = "CanManageMovies")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var viewModel = new SaveMovieViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("Save", viewModel);
        }
       
        public IActionResult Index()
        {
            if(User.IsInRole("CanManageMovies"))
                return View("Lists");

            return View("ReadOnlyLists");
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}