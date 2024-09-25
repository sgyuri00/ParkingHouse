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
    public class SumCostController : ControllerBase
    {
        ICarLogic carlogic;
        IParkolasLogic parlogic;
        IHubContext<SignalRHub> hub;
        public SumCostController(ICarLogic carlogic, IParkolasLogic parlogic, IHubContext<SignalRHub> hub)
        {
            this.carlogic = carlogic;
            this.parlogic = parlogic;
            this.hub = hub;
        }
        //noncrud3
        [HttpGet]
        public IEnumerable<SumCost> Parking()
        {
            return carlogic.ParkingFee();
            //this.hub.Clients.All.SendAsync("");
        }
    }
}
