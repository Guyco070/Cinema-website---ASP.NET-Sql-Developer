using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab1.Dal;
using lab1.ViewModel;
using lab1.Models;

namespace lab1.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addMovie()
        {
            MovieDal dalCust = new MovieDal();
            MovieViewModel mvm = new MovieViewModel();
            mvm.movie = new Movie();
            List<Movie> movies;

            movies = dalCust.Movies.ToList<Movie>();
            mvm.movies = movies;
            return View("addMovie", mvm);
        }
    }
}