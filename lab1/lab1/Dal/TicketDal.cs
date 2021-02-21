using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using lab1.Models;

namespace lab1.Dal
{
    public class TicketDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().ToTable("tblTickets");
        }

        public DbSet<Ticket> Tickets { get; set; }

        public void RemovePecked(Movie movie)
        {
            List<Ticket> toRemove = Tickets.Where(x => x.token == 1 || x.token == 0).ToList();
            foreach (Ticket ticket in toRemove)
                if(movie.movieDate.Equals(ticket.movieDate) && movie.movieTime.Equals(ticket.movieTime) && movie.hall.Equals(ticket.hall))
                    Tickets.Remove(ticket);

            SaveChanges();
        }
    }
}