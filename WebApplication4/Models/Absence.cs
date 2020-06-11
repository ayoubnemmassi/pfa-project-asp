using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class Absence
    {
        public int Absid { get; set; }
        public int SeanceId { get; set; }
        public int? StudentId { get; set; }
        public DateTime Date { get; set; }
    }
}
