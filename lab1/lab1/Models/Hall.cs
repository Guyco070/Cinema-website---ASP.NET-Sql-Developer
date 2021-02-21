using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Hall
    {
        //[Required(ErrorMessage = "Please enter type.")]
        [Required]
        [Range(0, 40, ErrorMessage = "Seat contain only number of seat (1-40)")]
        public int seats { get; set; }

        [Key]
        //[Required(ErrorMessage = "Please enter password.")]
        [Required]
        [RegularExpression("^VIP|vip|[1-3]$", ErrorMessage = "There are 4 halls - VIP/1/2/3.")]
        public string hallId { get; set; }
    }
}