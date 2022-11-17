using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlympicsStatistics.Models;

namespace OlympicsStatistics.Controllers
{
    public static class OlympicsController
    {
        public async static Task<List<string>> getAllNocs()
        {
            using (OlympicsContext context = new OlympicsContext())
            {
                return await context.Partecipations
                    .Select(s => s.NOC)
                    .Distinct()
                    .OrderBy(s=>s)
                    .ToListAsync();
            };
        }

        public async static Task<List<AthletesWithMedals>> getAthletes(string noc, bool onlyMedalists, int page, int pageSize)
        {
            using (OlympicsContext context = new OlympicsContext())
            {
                if (page <= 0) page = 1;


                return await context.AthletesNFs
                    .Where(q => noc == null || q.Partecipations.Any(s => s.NOC == noc))
                    .Select(s => new AthletesWithMedals
                    {
                        IdAthlete = s.IdAthlete,
                        Name = s.Name,
                        Golds = s.Medals.Where(t => t.Medal1 == "Gold").Count(),
                        Silvers = s.Medals.Where(t => t.Medal1 == "Silver").Count(),
                        Bronzes = s.Medals.Where(t => t.Medal1 == "Bronze").Count()
                    })
                    .Where(s => onlyMedalists ? (s.Golds + s.Silvers + s.Bronzes) > 0 : true)
                    .OrderByDescending(s => s.Golds)
                    .ThenByDescending(s => s.Silvers)
                    .ThenByDescending(s => s.Bronzes)
                    .ThenBy(s => s.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            
        }

        public async static Task AddNew()
        {
            using (OlympicsContext context = new OlympicsContext())
            {
                try
                {

                    //Inserisco Gianmarco Tamberi che ha vinto l'oro nel salto in alto nel 2021
                    //AthletesNF --> Recuperare l'Id dell'atleta
                    AthletesNF a = await context.AthletesNFs.FirstOrDefaultAsync(s => s.Name == "Gianmarco Tamberi");

                    //Events --> Recuperare la gara
                    Event e = await context.Events.FirstOrDefaultAsync(s=>s.Id == 575);

                    //Games --> Nuova Riga, non abbiamo i giochi del 2021
                    Game g = new Game
                    {
                        //Id è autoincrementale
                        Games = "2020 Summer",
                        Year = 2021,
                        Season = "Summer",
                    };
                    context.Games.Add(g);
                    await context.SaveChangesAsync();
                    //g = await context.Games.FirstOrDefaultAsync(s=> s.Year == 2021); // recuperato perchè non sappiamo l'id

                    //Partecipation --> Nuova riga
                    Partecipation p = new Partecipation
                    {
                        Id = await context.Partecipations.MaxAsync(s => s.Id) + 1,
                        IdAthlete = a.IdAthlete,
                        Age = 29,
                        NOC = "ITA",
                        IdEvent = e.Id,
                        IdGame = g.Id,
                        City = "Tokyo"
                    };

                    context.Partecipations.Add(p);

                    //Medals --> nuova riga
                    Medal m = new Medal
                    {
                        //IdAthlete = athlete.IdAthlete,
                        AthletesNF = a, // ha già tutti i mapping, quindi ci permette di lavorare object oriented
                        Event = e,
                        Game = g,
                        Medal1 = "Gold"
                    };

                    context.Medals.Add(m);
                    await context.SaveChangesAsync();


                    //Inseriamo tamberi anche nelle olimpiadi del 2024
                    Partecipation p2024 = new Partecipation
                    {
                        Id = await context.Partecipations.MaxAsync(s => s.Id) + 1,
                        AthletesNF = a,
                        Event = e,
                        Age = 33,
                        NOC = "ITA",
                        City = "Paris",
                        Game = new Game
                        {
                            Games = "2024 Summer",
                            Year = 2024,
                            Season = "Summer"
                        }
                    };

                    context.Partecipations.Add(p2024);
                    await context.SaveChangesAsync();

                    AthletesNF nuovoAtleta = new AthletesNF //le proprietà di navigazione valgono in tutti i versi
                    {
                        Name = "sdfsdf",
                        Medals = new List<Medal>
                        {
                            new Medal (),
                            new Medal(),
                        }
                    };
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }

}
