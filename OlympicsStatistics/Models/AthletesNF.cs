//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OlympicsStatistics.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AthletesNF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AthletesNF()
        {
            this.Medals = new HashSet<Medal>();
            this.Partecipations = new HashSet<Partecipation>();
        }
    
        public int IdAthlete { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public Nullable<short> Height { get; set; }
        public Nullable<short> Weight { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medal> Medals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partecipation> Partecipations { get; set; }
    }
}
