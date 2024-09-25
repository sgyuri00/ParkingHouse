using Microsoft.AspNetCore.Mvc;
using Parkolo.Logic;
using Parkolo.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Parkolo.Endpoint.Services;

namespace Parkolo.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        ICarLogic logic;
        IHubContext<SignalRHub> hub;
        public AutoController(ICarLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet/*("All")*/]
        public IEnumerable<Auto> ReadAll()
        {
            return this.logic.GetAllAuto();
            //hub.Clients.All.SendAsync("");
        }

        [HttpGet("{Rendszám}")]
        public Auto Read(string Rendszám)
        {
            return this.logic.GetByPlateNum(Rendszám);
        }

        [HttpPost]
        public void Create([FromBody] Auto auto)
        {
            //Auto auto = new Auto() { Rendszam = Rendszám, Marka = Márka, GyartasiEv = Gyártási_Év, Uzemanyag = Üzemanyag };
            this.logic.AddCar(auto);
            //hub.Clients.All.SendAsync("CarCreated", this.logic.GetByPlateNum(auto.Rendszam));
            this.hub.Clients.All.SendAsync("AutoCreated", auto);
            //hub.Clients.All.SendCoreAsync("CarCreated", auto);
        }

        [HttpPut]
        public void Put(Auto auto)
        {
            //Auto cartoupdate = this.logic.GetByPlateNum(auto.Rendszam);
            
            this.logic.ChangeFuel(auto.Rendszam, auto.Uzemanyag);
            hub.Clients.All.SendAsync("AutoUpdated", auto);
            //this.hub.Clients.All.SendAsync("CarUpdated", new Auto() { Rendszam = Rendszám, Uzemanyag = Üzemanyag });//maybeeeee
        }

        [HttpDelete("{Rendszám}")]
        public void Delete(string Rendszám)
        {
            var cartodelete = this.logic.GetByPlateNum(Rendszám);
            this.logic.DeleteCar(Rendszám);
            hub.Clients.All.SendAsync("AutoDeleted", cartodelete);
        }
    }
}
