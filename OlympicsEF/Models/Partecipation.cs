//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OlympicsEF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Partecipation
    {
        public long Id { get; set; }
        public Nullable<long> IdAthlete { get; set; }
        public Nullable<long> IdGame { get; set; }
        public Nullable<long> IdEvent { get; set; }
        public string City { get; set; }
        public Nullable<int> Age { get; set; }
        public string NOC { get; set; }
    
        public virtual AthletesNF AthletesNF { get; set; }
        public virtual Event Event { get; set; }
        public virtual Game Game { get; set; }
    }
}
