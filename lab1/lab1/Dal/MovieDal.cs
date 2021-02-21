using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using lab1.Models;


namespace lab1.Dal
{
    public class MovieDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().ToTable("tblMovies");
        }

        public DbSet<Movie> Movies { get; set; }
    }
}