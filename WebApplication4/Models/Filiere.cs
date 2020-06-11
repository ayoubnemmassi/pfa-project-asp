using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class Filiere
    {
        public Filiere()
        {
            Session = new HashSet<Session>();
        }

        public int FiliereId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Session> Session { get; set; }
    }
}
