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
    public class PaidMoreThan2000Controller : ControllerBase
    {
        ICarLogic carlogic;
        IParkolasLogic parlogic;
        IHubContext<SignalRHub> hub;
        public PaidMoreThan2000Controller(ICarLogic carlogic, IParkolasLogic parlogic, IHubContext<SignalRHub> hub)
        {
            this.carlogic = carlogic;
            this.parlogic = parlogic;
            this.hub = hub;
        }
        //noncrud1
        [HttpGet]
        public IEnumerable<PaidMoreThan2000> PeoplePaid()
        {
            return parlogic.PeoplePaidMoreThan2000();
        }
    }
}
