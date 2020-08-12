using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
    }
}
