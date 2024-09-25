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
    public class ParkoloSpotsController : ControllerBase
    {
        ICarLogic logic;
        IHubContext<SignalRHub> hub;
        public ParkoloSpotsController(ICarLogic logic , IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<ParkoloSpots> ReadAll()
        {
            return this.logic.GetAllParkolo();
        }

        [HttpGet("{id}")]
        public ParkoloSpots Read(int id)
        {
            return this.logic.GetBySpotId(id);
        }

        [HttpPost]
        public void Create([FromBody] ParkoloSpots park)
        {
            //ParkoloSpots p = new ParkoloSpots() { ParkolohelySzam = ParkolohelySzam, Meret = Méret, ElektromosE = Elektromos_E };
            this.logic.AddParkolo(park);
            this.hub.Clients.All.SendAsync("ParkoloSpotsCreated", park);
        }

        [HttpPut]
        public void Put(ParkoloSpots p)
        {
            this.logic.ChangeSpotSize(p.ParkolohelySzam, p.Meret);
            this.hub.Clients.All.SendAsync("ParkoloSpotsUpdated", p);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ParkoloSpots p = this.logic.GetBySpotId(id);
            this.logic.DeleteParkolo(id);
            this.hub.Clients.All.SendAsync("ParkoloSpotsDeleted", p);
        }
    }
}
