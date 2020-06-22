using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Message
    {
        public int id { get; set; }
        public int receverid { get; set; }

        public string clientuniqueid { get; set; }
        public string type { get; set; }
        public string message { get; set; } 
        public DateTime date { get; set; }
        public string image { get; set; }
    }
}
