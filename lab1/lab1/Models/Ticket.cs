using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class Ticket
    {

        [Required(ErrorMessage = "Please enter cost.")]
        [Range(0, 250, ErrorMessage = "Cost of ticket is 0-250 dollars.")]
        public int cost { get; set; }

        [Required]
        [Range(0, 2, ErrorMessage = "token of ticket is 0/1/2.")] // 0-red 1-green 2-grey
        public int token { get; set; }

        //[Required(ErrorMessage = "Please enter first name.")]
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Movie name must be at list 1 letters.")]
        public string movieName { get; set; }

        [Required]
        //[Required(ErrorMessage = "Please enter I.D Number.")]
        [RegularExpression("^(0?[1-9]{1}[0-9]{7,8})|(Default[0-9]*)$", ErrorMessage = "I.D number must be an number of 8-9 digits.")]
        public string idNumber { get; set; }

        //[Required/*(ErrorMessage = "Please enter last name.")*/]
        [Key]
        [Column(Order = 0)]
        [RegularExpression("^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$", ErrorMessage = "Movie Date in format - DD.MM.YYYY.")]
        public string movieDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        //[Required(ErrorMessage = "Please enter password.")]
        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = "Movie time can get only HH:MM format.")]
        public string movieTime { get; set; }
        [Key]
        [Column(Order = 2)]
        //[Required(ErrorMessage = "Please enter password.")]
        [Required]
        [RegularExpression("^VIP|vip|[1-3]$", ErrorMessage = "There are 4 halls - VIP/1/2/3.")]
        public string hall { get; set; }

        [Key]
        [Column(Order = 3)]
        //[Required(ErrorMessage = "Please enter type.")]
        [Required]
        [Range(0, 40, ErrorMessage = "Seat contain only number of seat (1-40)")]
        public int seat { get; set; }

        public Ticket()
        {
            token = 0;
        }

        public Ticket(string idNumber, int cost, string movieName, string movieDate, string movieTime, string hall, int seat) 
        {
            this.movieName = movieName;
            this.movieDate = movieDate;
            this.movieTime = movieTime;
            this.cost = cost;
            this.hall = hall;
            this.seat = seat;
            token = 1;
            this.idNumber = idNumber;
        }
    }
}