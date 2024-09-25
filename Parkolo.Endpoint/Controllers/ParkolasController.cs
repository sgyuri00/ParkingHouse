using Microsoft.AspNetCore.Mvc;
using Parkolo.Logic;
using Parkolo.Data;
using System.Collections.Generic;

namespace Parkolo.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParkolasController : ControllerBase
    {
        IParkolasLogic logic;
        public ParkolasController(IParkolasLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Parkolas> ReadAll()
        {
            return this.logic.GetAllParkolas();
        }

        [HttpGet("{Rendszám}")]
        public Parkolas Read(string Rendszám)
        {
            return this.logic.GetParkolasByRendszam(Rendszám);
        }

        [HttpPost]
        public void Create(int ParkolóId, int SzemélyId, int ParkolóhelySzám, string Rendszám, int EltöltöttIdő, int Költség)
        {
            this.logic.AddParkolas(new Parkolas() { ParkoloId = ParkolóId, SzemelyId = SzemélyId, ParkolohelySzam = ParkolóhelySzám, Rendszam = Rendszám, EltoltottIdo = EltöltöttIdő, Koltseg = Költség});
        }

        [HttpPut]
        public void Put(string Rendszám, int ÚjHely)
        {
            this.logic.ChangeParkolasSpot(Rendszám, ÚjHely);
        }

        [HttpDelete("{Rendszám}")]
        public void Delete(string Rendszám)
        {
            this.logic.DeleteParking(Rendszám);
        }
    }
}
