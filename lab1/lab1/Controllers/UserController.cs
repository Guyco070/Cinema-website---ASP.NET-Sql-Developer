using lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab1.Dal;
using lab1.ViewModel;
using System.Data.Entity.Validation;

namespace lab1.Controllers
{
    public class UserController : Controller
    {
        static User curUser = new User();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter()
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();

            List<User> users = dalCust.Users.ToList<User>();
            cvm.user = new User();
            cvm.users = users;

            return View(cvm);
        }

        public ActionResult sendButtonAction(User user)
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();

            if (ModelState.IsValid)
            {
                dalCust.Users.Add(user);
                dalCust.SaveChanges();

                //return View("User", user);
            }
            else
                cvm.user = user;

            cvm.users = dalCust.Users.ToList<User>();
            return View("Enter", cvm);
        }

        public ActionResult showSearch()
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();
            //List<User> users = new List<User>();
            cvm.users = dalCust.Users.ToList<User>();

            return View("SearchUser", cvm);
        }

        public ActionResult searchUser() // לא מקבל מערך אז חייב להריץ מ showSearch
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();

            string fNameSearchV = Request.Form["fNameSearch"].ToString();
            //string lNameSearchV = Request.Form["lNameSearch"].ToString();
            //string idNSearchV = Request.Form["idNSearch"].ToString();
            cvm.users = (from x in dalCust.Users where x.firstName.Contains(fNameSearchV) select x).ToList<User>();
            return View("SearchUser", cvm);
        }
        [HttpPost]
        public ActionResult SignInStart()
        {
            curUser = new User();
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();
            //List<User> users = new List<User>();
            cvm.users = dalCust.Users.ToList<User>();
            return View("SignIn", cvm);
        }

        public ActionResult SignIn() // לא מקבל מערך אז חייב להריץ מ SignInStart
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();
            
            string idNSearchV = Request.Form["idSearch"].ToString();
            string passSearchV = Request.Form["passSearch"].ToString();

            cvm.users = (from x in dalCust.Users where (x.idNumber.Equals(idNSearchV) && x.uPassword.Equals(passSearchV)) select x).ToList<User>();
            if (cvm.users.Count != 0) {
                cvm.user = cvm.users[0];
                curUser = cvm.users[0];
                if (curUser.uType.Equals("admin"))
                    return View("AdminStartView", cvm);
                else
                    return View("RegularUserStartView", cvm);
            }
                
            return SignInStart();
        }

        public ActionResult startPageButton()
        {
            UserViewModel cvm = new UserViewModel();
            cvm.user = curUser;
            if (curUser.uType.Equals("guest"))
                return MoviesSort();
            else if (curUser.uType.Equals("admin"))
                return View("AdminStartView", cvm);
            else
                return View("RegularUserStartView", cvm);
        }
        public ActionResult startPageButtonClear(Movie movie)
        {
            UserViewModel cvm = new UserViewModel();
            (new TicketDal()).RemovePecked(movie);
            cvm.user = curUser;
            if (curUser.uType.Equals("guest"))
                return MoviesSort();
            else if (curUser.uType.Equals("admin"))
                return View("AdminStartView", cvm);
            else
                return View("RegularUserStartView", cvm);
        }

        public ActionResult RemoveUser()
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();

            List<User> users = dalCust.Users.ToList<User>();
            cvm.user = new User();
            cvm.users = users;

            return View("RemoveUser",cvm);
        }
        
        public ActionResult removeThisUser(User user)
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();

            if (ModelState.IsValid)
            {
                cvm.users = ((from x in dalCust.Users where x.idNumber.Equals(user.idNumber) select x).ToList<User>());
                if (cvm.users.Count == 1)
                {
                    dalCust.Users.Remove(cvm.users[0]);
                    dalCust.SaveChanges();
                }

                //return View("User", user);
            }
            else
                cvm.user = user;

            cvm.users = dalCust.Users.ToList<User>();
            return RemoveUser();
        }

        public ActionResult searchUserRemove() 
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();

            string fNameSearchV = Request.Form["fNameSearch"].ToString();
            string lNameSearchV = Request.Form["lNameSearch"].ToString();
            string idNSearchV = Request.Form["idNSearch"].ToString();
            cvm.users = (from x in dalCust.Users where (x.firstName.StartsWith(fNameSearchV) && x.lastName.StartsWith(lNameSearchV) && x.idNumber.StartsWith(idNSearchV)) select x).ToList<User>();

            return View("RemoveUser", cvm);
        }
        [HttpPost]
        public ActionResult addUser()
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();
            cvm.user = new User();
            List<User> users;
            if (curUser.uType.Equals("admin"))
            {
                users = dalCust.Users.ToList<User>();
                cvm.users = users;
                return View("addUserAdmin", cvm);
            }
            users = (from x in dalCust.Users where x.uType.Equals("user") select x).ToList<User>();
            cvm.users = users;
            return View("addRegularUser", cvm);
        }
        [HttpPost]
        public ActionResult addUserButton(User user)
        {
            UserDal dalCust = new UserDal();
            UserViewModel cvm = new UserViewModel();

            if (ModelState.IsValid)
            {
                dalCust.Users.Add(user);
                dalCust.SaveChanges();

                //return View("User", user);
            }
            else
                cvm.user = user;

            cvm.users = dalCust.Users.ToList<User>();
            return addUser();
        }

        // to movie controller
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

        public ActionResult addMovieButton(Movie movie)
        {
            MovieDal dalCust = new MovieDal();
            MovieViewModel mvm = new MovieViewModel();

            if (ModelState.IsValid)
            {
                List<Movie> tempMovies = (dalCust.Movies.Where(x=> (x.movieName.Equals(movie.movieName)))).ToList<Movie>();
                
                if (HallToken(movie))
                {
                    if ((movie.movieImg == null || movie.movieImg.Equals("Default") || movie.movieImg.Equals("default") || movie.movieImg.Equals("")) && tempMovies.Count > 0)
                        movie.movieImg = tempMovies[0].movieImg;
                    else if (movie.movieImg == null || movie.movieImg.Equals(""))
                        movie.movieImg = "https://nrb.org/files/3315/7367/4295/film-and-board-1000.jpg";
                    movie.durationDisplay = movie.duration / 60 + "-h " + movie.duration % 60 + "-min";
                    dalCust.Movies.Add(movie);
                    dalCust.SaveChanges();
                    ViewBag.errorMS = "true";
                    mvm.movie = movie;
                }
                else
                {
                    ViewBag.errorMS = "false";
                    mvm.movie = movie;
                }
            }
            else
                mvm.movie = movie; 

            mvm.movies = dalCust.Movies.ToList<Movie>();
            return View("addMovie", mvm);
        }

        public bool HallToken(Movie movieToAdd)
        {
            List<Movie> hallMovieList = (new MovieDal()).Movies.Where(x => x.hall.Equals(movieToAdd.hall)).ToList<Movie>();
            DateTime movieToAddDate = DateTime.Parse(movieToAdd.movieDate + " " + movieToAdd.movieTime);
            foreach (Movie movie in hallMovieList)
            {
                DateTime after;
                DateTime before;
                if (movieToAdd.hall.Equals(movie.hall))
                {

                    after = DateTime.Parse(movie.movieDate + " " + movie.movieTime).AddMinutes(movie.duration + 30);
                    before = DateTime.Parse(movie.movieDate + " " + movie.movieTime).AddMinutes(-30);
                    if (movieToAddDate < after && movieToAddDate > before)
                        return false;
                }
            }
            return true;
        }
       
        public ActionResult removeThisMovie(Movie movie)
        {
            MovieDal dalCust = new MovieDal();
            MovieViewModel mvm = new MovieViewModel();

            mvm.movies = ((from x in dalCust.Movies where x.hall.Equals(movie.hall) && x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) select x).ToList<Movie>());
            if (mvm.movies.Count == 1)
            {
                dalCust.Movies.Remove(mvm.movies[0]);
                dalCust.SaveChanges();
            }
            else
                mvm.movie = movie;

            mvm.movies = dalCust.Movies.ToList<Movie>();
            return RemoveSearchMovie();
        }

        public ActionResult RemoveSearchMovie()
        {
            MovieDal dalCust = new MovieDal();
            MovieViewModel mvm = new MovieViewModel();
            string movieNameS = "";
            string movieDateS = "";
            string movieTimeS = "";
            string hallS = "";
            int costS = 0;
            if (Request.Form["movieNameS"] != null && Request.Form["movieNameS"] != null && Request.Form["movieNameS"] != null)
            {
                movieNameS = Request.Form["movieNameS"].ToString();
                movieDateS = Request.Form["movieDateS"].ToString();
                movieTimeS = Request.Form["movieTimeS"].ToString();
                hallS = Request.Form["hallS"].ToString();
                if (!Request.Form["costS"].ToString().Equals(""))
                    Int32.TryParse(Request.Form["costS"].ToString(), out costS);
            }
            mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost>=costS) select x).ToList<Movie>();

            return View("RemoveMovie", mvm);
        }

        public ActionResult searchMovie()
        {
            MovieDal dalCust = new MovieDal();
            MovieViewModel mvm = new MovieViewModel();

            string movieNameS = "";
            string movieDateS = "";
            string movieTimeS = "";
            if (Request.Form["movieNameS"] != null && Request.Form["movieNameS"] != null && Request.Form["movieNameS"] != null) {
                movieNameS = Request.Form["movieNameS"].ToString();
                movieDateS = Request.Form["movieDateS"].ToString();
                movieTimeS = Request.Form["movieTimeS"].ToString();
            }
            
            mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS)) select x).ToList<Movie>();

            return View("SearchMovie", mvm);
        }

        public ActionResult ChangeH_Or_COfThisMovie(Movie movie)
        {
            MovieDal dalCust = new MovieDal();
            MovieViewModel mvm = new MovieViewModel();

            if (ModelState.IsValid)
            {
                mvm.movies = ((from x in dalCust.Movies where x.movieName.Equals(movie.movieName) && x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) select x).ToList<Movie>());
                int costS = 0;
                if (mvm.movies.Count == 1)
                {
                    Movie tempM = mvm.movies[0];
                    dalCust.Movies.Remove(mvm.movies[0]);
                    dalCust.SaveChanges();
                    if (!Request.Form["newCostS"].ToString().Equals("") && ModelState.IsValid)
                    {
                        Int32.TryParse(Request.Form["newCostS"].ToString(), out costS);
                        tempM.cost = costS;
                    }
                    string tempHall = Request.Form["newHallS"].ToString();
                    if (!tempHall.Equals("") && ModelState.IsValid)
                        tempM.hall = tempHall;
                    dalCust.Movies.Add(tempM);
                    dalCust.SaveChanges();
                }
            }
            else
                mvm.movie = movie;

            mvm.movies = dalCust.Movies.ToList<Movie>();
            return RemoveSearchMovie();
        }

        // to movie controller (have to)
        public ActionResult MoviesSort()
        {
            MovieDal dalCust = new MovieDal();
            MovieViewModel mvm = new MovieViewModel();
            string movieNameS = "", movieDateS = "", movieTimeS = "", hallS = "", sortBy = "", filterBy = "", onSaleCB = "";
            int minCostS = 0, maxCostS = 0;
            if (Request.Form["movieNameS"] != null && Request.Form["movieNameS"] != null && Request.Form["movieNameS"] != null)
            {
                movieNameS = Request.Form["movieNameS"].ToString();
                movieDateS = Request.Form["movieDateS"].ToString();
                movieTimeS = Request.Form["movieTimeS"].ToString();
                hallS = Request.Form["hallS"].ToString();
                if (!Request.Form["minCostS"].ToString().Equals(""))
                    Int32.TryParse(Request.Form["minCostS"].ToString(), out minCostS);
                if (!Request.Form["maxCostS"].ToString().Equals(""))
                    Int32.TryParse(Request.Form["maxCostS"].ToString(), out maxCostS);
                if(minCostS > maxCostS && maxCostS != 0)
                {
                    int temp = minCostS;
                    minCostS = maxCostS;
                    maxCostS = temp;
                }
            }
            if (Request.Form["sortBy"] != null)
            {
                sortBy = Request.Form["sortBy"].ToString();
                if (sortBy == "Price decrease")
                {
                    if (maxCostS == 0)
                        mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS) select x).OrderBy(o => o.cost).ToList<Movie>();
                    else
                        mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS && x.cost <= maxCostS) select x).OrderBy(o => o.cost).ToList<Movie>();
                }
                else if (sortBy == "Price increase")
                {
                    if (maxCostS == 0)
                        mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS) select x).OrderByDescending(o => o.cost).ToList<Movie>();
                    else
                        mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS && x.cost <= maxCostS) select x).OrderByDescending(o => o.cost).ToList<Movie>();
                }
                else if (sortBy == "Most popular")
                {
                    if (maxCostS == 0)
                        mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS) select x).OrderByDescending(o => o.popularity).ToList<Movie>();
                    else
                        mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS && x.cost <= maxCostS) select x).OrderByDescending(o => o.popularity).ToList<Movie>();
                }
            }else if (filterBy.Equals("") && onSaleCB.Equals("") && movieNameS.Equals("") && movieDateS.Equals("") && movieTimeS.Equals("") && hallS.Equals("") && sortBy.Equals("") && maxCostS == 0)
                mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS) select x).OrderBy(o => o.cost).ToList<Movie>();
            else
                mvm.movies = (from x in dalCust.Movies where (x.movieName.StartsWith(movieNameS) && x.movieDate.StartsWith(movieDateS) && x.movieTime.StartsWith(movieTimeS) && x.hall.StartsWith(hallS) && x.cost >= minCostS && x.cost <= maxCostS) select x).OrderBy(o => o.cost).ToList<Movie>();

            if (Request.Form["filterBy"] != null)
            {
                filterBy = Request.Form["filterBy"].ToString();
                if(filterBy != "All")
                        mvm.movies = mvm.movies.Where(x => x.category.Contains(filterBy)).ToList<Movie>();
            }
            if (Request.Form["showAllDatesCB"] == null)
            {
                List<Movie> tempList = new List<Movie>();
                foreach (Movie movie in mvm.movies)
                {
                    if (tempList == null || (tempList.Where(x => x.movieName == movie.movieName).ToList<Movie>().Count) == 0)
                        tempList.Add(returnLastShow(movie, mvm.movies));
                }
                mvm.movies = tempList;
                tempList = null;
            }

                mvm.movies.Where(m => m.sale > 0).ToList<Movie>().ForEach(s => s.newCost = changePrice(s.cost, s.sale)); // change to new cost
            
            //mvm.movies.Where(m => m.movieDate).ToList<Movie>().ForEach(s => s.newCost = changePrice(s.cost, s.sale)); // filter by date (last)

            if (Request.Form["onSaleCB"] != null)
            {    
                    mvm.movies = mvm.movies.Where(x => x.sale > 0).ToList<Movie>();
            }

            mvm.movies = mvm.movies.Where(x=> onlyNotPassedMovies(x)).ToList<Movie>();

            return View("MoviesViwer", mvm);
        }

        public bool onlyNotPassedMovies(Movie movie)
        {
            DateTime lastDate = DateTime.Now;
            DateTime date;
            date = DateTime.Parse(movie.movieDate + " " + movie.movieTime);
            return lastDate < date;
        }

        public double changePrice(int cost, int sale)
        {
            double newCost = (cost * (1.0 - ((double)sale / 100)));
            return newCost;
        }

        public Movie returnLastShow(Movie lastMovie, List<Movie> movies)
        {
            foreach (Movie movie in movies)
            {
                DateTime lastDate = DateTime.Parse(lastMovie.movieDate);
                DateTime date;
                    if (lastMovie.movieName.Equals(movie.movieName))
                    {
                        date = DateTime.Parse(movie.movieDate + " " + movie.movieTime);
                        if (lastDate < date)
                            lastMovie = movie;
                    }
            }
            return lastMovie; 
        }
        
        // to Ticket controller


        public ActionResult buyATicket(Movie movie)
                {
                    MovieDal dalCust = new MovieDal();
                    MTHViewModel mvm = new MTHViewModel();
                    TicketDal dalCustT = new TicketDal();

                    mvm.tickets = (from x in dalCustT.Tickets where x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) select x).ToList<Ticket>();
                    mvm.movies = ((from x in dalCust.Movies where x.movieName.Equals(movie.movieName) && x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) select x).ToList<Movie>());
                    mvm.movies.Where(m => m.sale > 0).ToList<Movie>().ForEach(s => s.newCost = changePrice(s.cost, s.sale)); // change to new cost
                    mvm.movie = mvm.movies[0];
                    mvm.movies = dalCust.Movies.ToList<Movie>();
                    HallViewModel mvh = new HallViewModel();
                    HallDal dalCustH = new HallDal();
                    mvh.hall = ((from x in dalCustH.Halls where x.hallId.Equals(movie.hall) select x).ToList<Hall>())[0];
                    ViewBag.hallSeats = mvh.hall.seats;
                    ViewBag.Peacked = 0;
                    mvm.hall = mvh.hall;     
                    return View("BuyATicketView", mvm);
                }

        public ActionResult peackASeat(Movie movie)
        {
            HallDal dalCustH = new HallDal();
            TicketDal dalCustT = new TicketDal();
            MovieDal dalCustM = new MovieDal();
            MTHViewModel mvm = new MTHViewModel();
            
            mvm.tickets = (from x in dalCustT.Tickets where x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) select x).ToList<Ticket>();

            mvm.hall = ((from x in dalCustH.Halls where x.hallId.Equals(movie.hall) select x).ToList<Hall>())[0];
            mvm.movies = ((from x in dalCustM.Movies where x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) select x).ToList<Movie>());

            mvm.movies.Where(m => m.sale > 0).ToList<Movie>().ForEach(s => s.newCost = changePrice(s.cost, s.sale)); // change to new cost
            mvm.movie = mvm.movies[0];
            mvm.movies = dalCustM.Movies.ToList<Movie>();
            
            if (Request.Form["seat"] != null && Request.Form["token"] != null)
            {
                int seat = Int32.Parse(Request.Form["seat"].ToString());
                int token = Int32.Parse(Request.Form["token"].ToString());

                Ticket ticket;

                mvm.tickets = (from x in dalCustT.Tickets where x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) && x.seat == seat select x).ToList<Ticket>();

                if (mvm.tickets.Count != 0)
                {
                    ticket = mvm.tickets[0];
                    if (curUser != null && ticket.idNumber.Contains("Default")) ticket.idNumber = curUser.idNumber;
                    
                    if (ticket.token == 1)
                    {
                        ticket = (from x in dalCustT.Tickets where x.movieDate.Equals(ticket.movieDate) && x.movieTime.Equals(ticket.movieTime) && x.hall.Equals(ticket.hall) && x.seat == ticket.seat select x).ToList<Ticket>()[0];
                        dalCustT.Tickets.Remove(ticket);
                        try
                        {
                            dalCustT.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }else
                {
                    ticket = new Ticket(curUser.idNumber, mvm.movie.cost, mvm.movie.movieName, mvm.movie.movieDate, mvm.movie.movieTime, mvm.movie.hall, seat);
                    dalCustT.Tickets.Add(ticket);
                    try
                    {
                        dalCustT.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            if(curUser!=null)
                mvm.tickets = (from x in dalCustT.Tickets where ((x.idNumber.Equals(curUser.idNumber) || x.token == 2) && x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall)) select x).ToList<Ticket>();
            else mvm.tickets = (from x in dalCustT.Tickets where (x.token == 2 && x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall)) select x).ToList<Ticket>();


            if ((from x in dalCustT.Tickets where x.idNumber.Equals(curUser.idNumber) && x.token == 1 && x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) select x).ToList<Ticket>().Count > 0)
                ViewBag.addShoppingCart = "true";
            else ViewBag.addShoppingCart = "false";

            ViewBag.hallSeats = mvm.hall.seats;

            return View("BuyATicketView", mvm);
        }

        public ActionResult isPayNow(Movie movie)
        {
            HallDal dalCustH = new HallDal();
            TicketDal dalCustT = new TicketDal();
            MovieDal dalCustM = new MovieDal();
            MTHViewModel mvm = new MTHViewModel();

            mvm.tickets = (from x in dalCustT.Tickets where x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) select x).ToList<Ticket>();

            mvm.hall = ((from x in dalCustH.Halls where x.hallId.Equals(movie.hall) select x).ToList<Hall>())[0];
            mvm.movies = ((from x in dalCustM.Movies where x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.hall.Equals(movie.hall) select x).ToList<Movie>());

            mvm.movies.Where(m => m.sale > 0).ToList<Movie>().ForEach(s => s.newCost = changePrice(s.cost, s.sale)); // change to new cost
            mvm.movie = mvm.movies[0];
            mvm.movies = dalCustM.Movies.ToList<Movie>();

            return View(mvm);
        }

        public ActionResult enterToShoppingCart()
        {
            MTHViewModel mth = new MTHViewModel();
            MovieDal dalCustM = new MovieDal();
            TicketDal dalCustT = new TicketDal();
            mth.user = curUser;
            mth.tickets = (from x in dalCustT.Tickets where x.idNumber.Equals(mth.user.idNumber) select x).ToList<Ticket>();
            foreach(Ticket ticket in mth.tickets)
                mth.movies.Add((from x in dalCustM.Movies where x.movieDate.Equals(ticket.movieDate) && x.movieTime.Equals(ticket.movieTime) && x.hall.Equals(ticket.hall) select x).ToList<Movie>()[0]);

            mth.movies.Where(m => m.sale > 0).ToList<Movie>().ForEach(s => s.newCost = changePrice(s.cost, s.sale)); // change to new cost
            if(mth.movies.Count != 0)
                mth.movie = mth.movies[0];
            if (mth.tickets.Count == 0)
                return View("EmptyCartView", mth);
            if (Request.Form["isPayNow"] != null)
            {
                string isPayNow = Request.Form["isPayNow"].ToString();
                if (isPayNow.Equals("Pay now") || isPayNow.Equals("Go to Cart"))
                    return View("ShopingCart", mth);
                else MoviesSort();
            }
            return MoviesSort();
        }


        public ActionResult removeThisFromShopingCart(Movie movie)
        {
            TicketDal dalCustT = new TicketDal();
            TicketViewModel tvm = new TicketViewModel();
            MTHViewModel mth = new MTHViewModel();
            MovieDal dalCustM = new MovieDal();

            tvm.tickets = ((from x in dalCustT.Tickets where x.idNumber.Equals(curUser.idNumber) && x.hall.Equals(movie.hall) && x.movieDate.Equals(movie.movieDate) && x.movieTime.Equals(movie.movieTime) && x.token == 1 select x).ToList<Ticket>());
            if (tvm.tickets.Count != 0)
            {
                foreach (Ticket ticket in tvm.tickets)
                    dalCustT.Tickets.Remove(ticket);
                dalCustT.SaveChanges();
            }
            mth.user = curUser;
            mth.tickets = (from x in dalCustT.Tickets where x.idNumber.Equals(mth.user.idNumber) select x).ToList<Ticket>();
            foreach (Ticket ticket in mth.tickets)
                mth.movies.Add((from x in dalCustM.Movies where x.movieDate.Equals(ticket.movieDate) && x.movieTime.Equals(ticket.movieTime) && x.hall.Equals(ticket.hall) select x).ToList<Movie>()[0]);
            if (mth.movies.Count != 0)
                mth.movie = mth.movies[0];
            else
                return MoviesSort();
            tvm.tickets = dalCustT.Tickets.ToList<Ticket>();
            return View("ShopingCart", mth);
        }

        public ActionResult pay(Movie movie)
        {
            MTHViewModel mth = new MTHViewModel();
            MovieDal dalCustM = new MovieDal();
            TicketDal dalCustT = new TicketDal();
            mth.user = curUser;
            mth.tickets = (from x in dalCustT.Tickets where x.idNumber.Equals(mth.user.idNumber) select x).ToList<Ticket>();
          
            foreach (Ticket ticket in mth.tickets)
                mth.movies.Add((from x in dalCustM.Movies where x.movieDate.Equals(ticket.movieDate) && x.movieTime.Equals(ticket.movieTime) && x.hall.Equals(ticket.hall) select x).ToList<Movie>()[0]);

            foreach (Ticket ticket in mth.tickets)
            {
                foreach (Movie movie1 in mth.movies)
                {
                    if (movie1.movieName.Equals(ticket.movieName))
                    {
                        dalCustT.Tickets.Remove(ticket);
                        try
                        {
                            dalCustT.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            Console.WriteLine(e);
                        }
                        dalCustM.Movies.Remove(movie1);
                        movie.popularity++;
                        dalCustM.Movies.Add(movie1);
                        try
                        {
                            dalCustM.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            Console.WriteLine(e);
                        }
  
                        ticket.token = 2;
                        dalCustT.Tickets.Add(ticket);
                        try
                        {
                            dalCustT.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            mth.movies.Where(m => m.sale > 0).ToList<Movie>().ForEach(s => s.newCost = changePrice(s.cost, s.sale)); // change to new cost
            mth.movie = mth.movies[0];
            

            return View("Confirmation",mth);
        }

        public ActionResult ChangeThisHallSeats(Hall hall)
        {
            HallDal dalCust = new HallDal();
            HallViewModel hvm = new HallViewModel();

            hvm.halls = ((from x in dalCust.Halls where x.hallId.Equals(hall.hallId) select x).ToList<Hall>());
            int seats = 0;
            if (hvm.halls.Count == 1)
            {
                Hall tempM = hvm.halls[0];
                dalCust.Halls.Remove(hvm.halls[0]);
                dalCust.SaveChanges();
                if (!Request.Form["newSeats"].ToString().Equals("") && ModelState.IsValid)
                {
                    Int32.TryParse(Request.Form["newSeats"].ToString(), out seats);
                    tempM.seats = seats;
                }
                dalCust.Halls.Add(tempM);
                dalCust.SaveChanges();
            }
      

            hvm.halls = dalCust.Halls.ToList<Hall>();
            return changeHall();
        }

        public ActionResult changeHall()
        {
            HallDal dalCust = new HallDal();
            HallViewModel mvm = new HallViewModel();
            string hallS = "";
            int seatS = 0;
            if (Request.Form["hallS"] != null && Request.Form["seatS"] != null)
            {
                hallS = Request.Form["hallS"].ToString();
                if (!Request.Form["seatS"].ToString().Equals(""))
                    Int32.TryParse(Request.Form["seatS"].ToString(), out seatS);
            }
            mvm.halls = dalCust.Halls.ToList<Hall>();

            return View("HallManageView", mvm);
        }
    }
}

