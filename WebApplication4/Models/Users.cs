using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImgPath { get; set; }
    }
}
