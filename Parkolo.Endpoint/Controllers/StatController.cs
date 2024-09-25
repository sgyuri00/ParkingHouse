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
    public class StatController : ControllerBase
    {
        ICarLogic carlogic;
        IParkolasLogic parlogic;
        IHubContext<SignalRHub> hub;
        public StatController(ICarLogic carlogic, IParkolasLogic parlogic, IHubContext<SignalRHub> hub)
        {
            this.carlogic = carlogic;
            this.parlogic = parlogic;
            this.hub = hub;
        }
        //noncrud1
        [HttpGet("PeoplePaidMoreThan2000")]
        public IEnumerable<PaidMoreThan2000> PeoplePaid()
        {
            return parlogic.PeoplePaidMoreThan2000();
        }

        //noncrud2
        [HttpGet("MalePeople")]
        public IEnumerable<MalePeople> MalePeople()
        {
            return parlogic.MalePeopleHaveParking();
        }

        //noncrud3
        [HttpGet("SumCostByCars")]
        public IEnumerable<SumCost> Parking()
        {
            return carlogic.ParkingFee();
            //this.hub.Clients.All.SendAsync("");
        }

        //noncrud4
        [HttpGet("UsedElectricParkingSpots")]
        public IEnumerable<UsedParkingSpots> UsedSpots()
        {
            return carlogic.UsedSpotsSizeOver13();
        }

        //noncrud5
        [HttpGet("ParkingOver8Hours")]
        public IEnumerable<Over8Hours> ParkingOver()
        {
            return carlogic.Over8Hours();
        }
    }
}
