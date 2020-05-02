using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Data
{
    public class MovieRentDbContext : DbContext
    {
        public MovieRentDbContext(DbContextOptions<MovieRentDbContext> options)
            :base(options) 
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
