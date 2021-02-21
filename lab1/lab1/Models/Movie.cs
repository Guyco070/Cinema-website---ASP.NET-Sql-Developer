using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class Movie
    {
        //[Required(ErrorMessage = "Please enter password.")]
        [Required]
        [Range(0, 1000, ErrorMessage = "Movie sale is an integer from 0 to 100 only.")]
        public double newCost { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        [Required]
        [Range(0, 100, ErrorMessage = "Movie sale is an integer from 0 to 100 only.")]
        public int sale { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        [Required]
        [Range(0, 200, ErrorMessage = "Movie age is an integer only.")]
        public int popularity { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        [Required]
        [Range(0, 120, ErrorMessage = "Movie age is an integer only. (0-120)")]
        public int minAge { get; set; }

        [Range(0, 500, ErrorMessage = "Movie duration is an integer only. (0-500 - minutes)")]
        public int duration { get; set; }

        public string durationDisplay { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        [Required]
        [RegularExpression("^([,]?[A-Za-z])*$", ErrorMessage = "Movie category can be only letters wuth a ',' between eche other.")]
        public string category { get; set; }

        //[Required(ErrorMessage = "Please enter I.D Number.")]
        [Required]
        [Range(0, 250, ErrorMessage = "Cost of ticket is 0-250 dollars.")]
        public int cost { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        //[RegularExpression("^VIP|vip|[1-3]$", ErrorMessage = "There are 4 halls - VIP/1/2/3.")]
        public string movieImg { get; set; }

        //[Required(ErrorMessage = "Please enter first name.")]
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Movie name must be at list 1 letters.")]
        public string movieName { get; set; }

        //[Required/*(ErrorMessage = "Please enter last name.")*/]
        [Key] [Column(Order = 0)]
        [Required]
        [RegularExpression("^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$", ErrorMessage = "Movie Date in format - DD.MM.YYYY.")]
        public string movieDate { get; set; }

        [Key] [Column(Order = 1)]
        //[Required(ErrorMessage = "Please enter password.")]
        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = "Movie time can get only HH:MM format.")]
        public string movieTime { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        [Key][Column(Order = 2)]
        [Required]
        [RegularExpression("^VIP|vip|[1-3]$", ErrorMessage = "There are 4 halls - VIP/1/2/3.")]
        public string hall { get; set; }


        public Movie(){
            movieImg = "";
            popularity = 0;
            sale = 0;
            newCost = 0;
            minAge = 18;
            durationDisplay = duration / 60 + "-h " + duration % 60 + "-min";
        }
    }
}