using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class SaveMovieViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        
        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        public int? Stock { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public SaveMovieViewModel()
        {
            Id = 0;   
        }

        public SaveMovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            Stock = movie.Stock;
            GenreId = movie.GenreId;
        }
        
    }
}
