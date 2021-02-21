using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace lab1.Models
{
    public class User
    {
        static int i = 0;

        //[Required(ErrorMessage = "Pleas enter first name.")]
        [StringLength(50,MinimumLength = 2, ErrorMessage = "The name must be at list 2 letters.")]
        public string firstName { get; set; }

        //[Required/*(ErrorMessage = "Pleas enter last name.")*/]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must be at list 2 letters.")]
        public string lastName { get; set; }

        //[Required(ErrorMessage = "Pleas enter type.")]
        [RegularExpression("^user|admin|guest$", ErrorMessage = "The type must be user or admin.")]
        public string uType { get; set; }

        //[Required(ErrorMessage = "Pleas enter password.")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "The passwors must 6-8 and contains only letters or numbers.")]
        public string uPassword { get; set; }

        [Key]
        //[Required(ErrorMessage = "Pleas enter I.D Number.")]
        [RegularExpression("^(0?[1-9]{1}[0-9]{7,8})|(Default[0-9]*)$", ErrorMessage = "I.D number must be an number of 8-9 digits.")]
        public string idNumber { get; set; }
        
        public User()
        {
            idNumber = "Default" + i;
            uType = "guest";
        }
    }
}