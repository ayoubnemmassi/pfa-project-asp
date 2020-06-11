using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
    }
}
