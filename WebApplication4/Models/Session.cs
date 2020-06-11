using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class Session
    {
        public int SeanceId { get; set; }
        public int FiliereId { get; set; }
        public int Profid { get; set; }
        public string Name { get; set; }
        public bool? State { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public string Day { get; set; }

        public virtual Filiere Filiere { get; set; }
        public virtual Professor Prof { get; set; }
    }
}
