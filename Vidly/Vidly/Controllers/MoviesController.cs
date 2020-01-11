using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Random(int id)
        {
            Movie movie = new Movie
            {
                Id = 101,
                Name = "Shrek!"
            };
            return View(movie);
        }
    }
}