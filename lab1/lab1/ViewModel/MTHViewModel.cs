using lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lab1.ViewModel
{
    public class MTHViewModel
    {
        public Ticket ticket { get; set; }
        public List<Ticket> tickets { get; set; }
        public List<Ticket> peackedS { get; set; }

        public Movie movie { get; set; }
        public List<Movie> movies { get; set; }

        public Hall hall { get; set; }
        public List<Hall> halls { get; set; }

        public User user { get; set; }
        public List<User> users { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must be at list 2 letters.")]
        public string creditCard { get; set; }

        [Required]
        [Range(100,999, ErrorMessage = "The name must be at list 2 letters.")]
        public int cvc { get; set; }

        [Required]
        public string exD { get; set; }

        public MTHViewModel()
        {
            movies = new List<Movie>();
        }
    }
}