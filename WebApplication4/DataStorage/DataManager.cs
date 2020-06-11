using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.DataStorage
{
    public static class DataManager
    {
        public static List<Chat> GetData()
        {
            var r = new Random();
            return new List<Chat>()
        {
           new Chat { Data = new List<int> { r.Next(1, 40) }, Label = "Data1" },
           new Chat { Data = new List<int> { r.Next(1, 40) }, Label = "Data2" },
           new Chat { Data = new List<int> { r.Next(1, 40) }, Label = "Data3" },
           new Chat { Data = new List<int> { r.Next(1, 40) }, Label = "Data4" }
        };
        }
    }
}
