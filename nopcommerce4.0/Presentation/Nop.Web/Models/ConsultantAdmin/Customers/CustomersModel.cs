﻿using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.ConsultantAdmin.Customers
{
    public class CustomersModel:BaseNopEntityModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        
        public string Email { get; set; }

        public string Mobile { get; set; }
         
        public string CustomerRole { get; set; }
        public string LastIpAddress { get; set; }
        public bool Active { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
    }
}
