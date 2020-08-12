using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private readonly MovieRentDbContext _context;

        public NewRentalsController(MovieRentDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            if (newRentalDto.MovieIds.Count == 0)
                return BadRequest("No Movie Ids have been given.");

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerId);

            if (customer == null)
                return BadRequest("Customer is not valid!");


            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRentalDto.MovieIds.Count)
                return BadRequest("One or move MovieIds are invalid.");

            foreach (var movie in movies)
            {
                if (movie.Available == 0)
                    return BadRequest("Not available this movie");

                movie.Available--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);

            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
