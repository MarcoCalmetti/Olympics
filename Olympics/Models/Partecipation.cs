using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Models
{
    class Partecipation
    {
        long Id { get; set; }
        long? IdAthlete { get; set; }
        string Name { get; set; }
        char? Sex { get; set; }
        int? Age { get; set; }
        int? Height { get; set; }
        int? Weight { get; set; }
        string NOC { get; set; }
        string Games { get; set; }
        int? Year { get; set; }
        string Season { get; set; }
        string City { get; set; }
        string Sport { get; set; }
        string Event { get; set; }
        string Medal { get; set; }
    }
}
