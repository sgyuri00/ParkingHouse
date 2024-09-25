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
    public class Over8HoursController : ControllerBase
    {
        ICarLogic carlogic;
        IParkolasLogic parlogic;
        IHubContext<SignalRHub> hub;
        public Over8HoursController(ICarLogic carlogic, IParkolasLogic parlogic, IHubContext<SignalRHub> hub)
        {
            this.carlogic = carlogic;
            this.parlogic = parlogic;
            this.hub = hub;
        }
        //noncrud5
        [HttpGet]
        public IEnumerable<Over8Hours> ParkingOver()
        {
            return carlogic.Over8Hours();
        }
    }
}

