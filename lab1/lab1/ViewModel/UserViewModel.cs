using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab1.Models;

namespace lab1.ViewModel
{
    public class UserViewModel
    {
        public User user { get; set; }

        public List<User> users { get; set; }
    }
}