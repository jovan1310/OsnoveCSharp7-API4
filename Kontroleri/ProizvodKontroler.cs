using Microsoft.AspNetCore.Mvc;
using OsnoveCSharp7_API4.Models;
using System.Collections.Generic;
using System.Linq;
namespace OsnoveCSharp7_API4.Kontroleri
{
    [Route("api/proizvodi")]
    [ApiController]
    public class ProizvodKontroler : Controller
    {
    
        // Lista gde ce se cuvati svi proizvodi:
        private static List<Proizvod> listaProizvoda = new List<Proizvod>();

        // Ruta za prikazivanje svih proizvoda:
        [HttpGet]
        public ActionResult<List<Proizvod>> PrikaziSve() => listaProizvoda;
        // Ruta za prikaz konkretnog proizvoda:
        [HttpGet("{proizvodID}")]
        public ActionResult<Proizvod> PrikazProizvodID(int proizvodID)
        {
            var proizvod = listaProizvoda.FirstOrDefault(p => p.ProizvodID == proizvodID);
            return proizvod != null ? Ok(proizvod) : NotFound();
        }
        // Ruta za dodavanje novog proizvoda:
        [HttpPost]
        public ActionResult<Proizvod> DodajProizvod(Proizvod noviProizvod)
        {
            noviProizvod.ProizvodID = listaProizvoda.Count + 1;
            listaProizvoda.Add(noviProizvod);
            return CreatedAtAction(nameof(PrikazProizvodID), new { proizvodID = noviProizvod.ProizvodID }, noviProizvod);
        }
        // Ruta za izmenu postojeceg proizvoda:
        [HttpPut("{proizvodID}")]
        public ActionResult IzmenaProizvoda(int proizvodID, Proizvod izmenjeniProizvod)
        {
            var proizvod = listaProizvoda.FirstOrDefault(p => p.ProizvodID == proizvodID);
            if (proizvod == null) return NotFound();
            
                proizvod.ProizvodIme = izmenjeniProizvod.ProizvodIme;
                proizvod.ProizvodOpis = izmenjeniProizvod.ProizvodOpis;
                proizvod.ProizvodKolicina = izmenjeniProizvod.ProizvodKolicina;
                return Ok(proizvod);
        }
        // Ruta za brsanje postojeceg proizvoda:
        [HttpDelete("{proizvodID}")]
        public ActionResult BrisanjeProizvoda(int proizvodID)
        {
            var proizvod = listaProizvoda.FirstOrDefault(p => p.ProizvodID == proizvodID);
            if (proizvod == null)
            {
                return NotFound();
            }
            else
            {
                listaProizvoda.Remove(proizvod);
                return NoContent();
            }
        }
    }
}

