﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab1.Models;

namespace lab1.ViewModel
{
    public class TicketViewModel
    {
        public Ticket ticket { get; set; }

        public List<Ticket> tickets { get; set; }
    }
}