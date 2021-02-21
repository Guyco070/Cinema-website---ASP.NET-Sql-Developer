using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab1.Models;

namespace lab1.ViewModel
{
    public class MovieViewModel
    {
        public Movie movie { get; set; }

        public List<Movie> movies { get; set; }
    }
}