using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Chat
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }

        public Chat() { Data = new List<int>(); }
    }
}
