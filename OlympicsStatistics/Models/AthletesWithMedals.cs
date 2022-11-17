using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicsStatistics.Models
{
    public class AthletesWithMedals
    {
        public long IdAthlete { get; set; }
        public string Name { get; set; }
        public int Golds { get;  set; }
        public int Silvers { get; set; }
        public int Bronzes { get; set; }
        public int Totals { get { return Golds + Silvers + Bronzes; } }
    }
}
