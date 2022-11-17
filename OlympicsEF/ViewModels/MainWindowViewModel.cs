using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlympicsEF.Models;
using System.Data.Entity;

namespace OlympicsEF.ViewModels
{
    class MainWindowViewModel 
    {
        private List<athlete> _dati;

        public List<athlete> Dati
        {
            get { return _dati; }
            set { _dati = value; }
        }

        public MainWindowViewModel()
        {
            using (OlympicsContext context = new OlympicsContext())
            {

                //var v = context.athletes
                //    //.Where(q => q.IdAthlete == 7)
                //    .Where(q => q.Year == 1992) // possiamo mettere più where e verranno poi utilizzati con l'AND
                //    .OrderBy(ob => ob.IdAthlete).ThenByDescending(ob => ob.IdAthlete)
                //    .Skip(10) // salta i primi 10
                //    .Take(10) // top 1
                //    .ToList();

                //athlete primo = context.athletes
                //    .OrderBy(ob => ob.Name)
                //    //.First(); //Take(1).ToList()[0] , esiste anche il .Last()
                //    //.First(i => i.Name == "Matteo") //condizione del where all'interno del First
                //    .FirstOrDefault(); //se non esiste, restituisce un null

                ////Età media dei medagliati
                //double? average = context.athletes
                //    .Where(i => i.Medal != null)
                //    .Where(q=> q.Age != null)
                //    .Average(a => a.Age);

                ////Elenco delle nazioni partecipanti
                //var nazioni /* dato che utilizziamo una classe generata al volo, dobbbiamo utlizzare var*/= context.athletes.Select(s => new 
                //{
                //    Nazione = s.NOC //Per la select andiamo ad creare una classe con i campi che vogliamo visualizzare, Nazione è solo un nome che diamo noi, possiamo anche non scriverlo
                //    //s.Noc
                //}).Distinct().OrderBy(ob => ob.Nazione).ToList(); //possiamo anche creare una classe specifica, esempio Nazione, per poi creare l'oggeto con new Nazione, per non avere più una lista generica

                //var noc = context.athletes.Where(q => q.Year == 2012)
                //    .GroupBy(g => new
                //    {
                //        g.NOC
                //    }).Select(s => new { s.Key.NOC, /*con key accediamo a tutti i campi nel GroupBy*/ part = s.Count() }).ToList();

                ////Tutti i medagliati
                //var q1 = context.AthletesNFs.Include(i => i.Medals).Where(s=>s.Medals.Count() > 0).ToList();

                ////tutti i medagliati d'oro
                //var q2 = context.AthletesNFs.Where(q => q.Medals.Any(s=>s.Medal1 == "Gold")).ToList(); // se voglio fare il filtro su un'altra tabella collegata con join

                //List<Medal> q3 = context.Medals.Include(i=>i.AthletesNF).ToList();
                //Dati = q3;


                //Selezione i 10 atleti più giovani ad aver vinto una medaglia d'oro
                var q4 = context.athletes
                    .Where(s=>s.Medal == "Gold" && s.Age != null)
                    .Select (s=> new { s.IdAthlete, s.Name, s.Age})
                    .Distinct()
                    .OrderBy(s => s.Age)
                    .Take(10)
                    .ToList();

                //Recuperare l'elenco delle città che hanno ospitato i giochi, in ordine alfabetico
                var q5 = context.athletes
                    .Select(s=>new { s.City})
                    .Distinct()
                    .OrderBy(s=>s.City)
                    .ToList();

                //Quanti atleti hanno vinto la medaglia d'oro?
                var q6 = context.athletes
                    .Select(i=> new { i.IdAthlete, i.Medal})
                    .Where(i => i.Medal == "Gold")
                    .Distinct()
                    .Count();

                //Per ogni città calcolare quante edizioni ha ospitato. Ordinare in ordine decrescente

                var q7 = context.athletes
                    .Select(j=> new { j.City, j.Games})
                    .GroupBy(i => new { i.City})
                    .Select(s => new { s.Key.City, Contatore = s.Distinct().Count()})
                    .OrderByDescending(ob => ob.Contatore)
                    .ThenBy(ob=> ob.City)
                    .ToList();

                //Stilare la classifica degli sport che hanno assegnato più medaglie

                var q8 = context.Medals
                    .Include(i=> i.Event)
                    .Select(j=> new { j.Event.Sport})
                    .GroupBy(s=>new { s.Sport })
                    .Select(s=>new { s.Key.Sport, Contatore = s.Count()})
                    .OrderByDescending(ob=>ob.Contatore)
                    .ToList();

                //Per ogni edizione calcolare il numero totale di medaglie assegnate
                var q9 = context.Medals
                    .Select(j => j.Game.Games) // dato che c'è solo un valore, posso non creare una classe
                    .GroupBy(gb => gb )
                    .Select(s => new { s.Key, Contatore = s.Count() })
                    .OrderByDescending(ob => ob.Contatore)
                    .ToList();

            }
        }

    }
}
