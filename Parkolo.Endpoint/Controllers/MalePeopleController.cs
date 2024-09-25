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
    public class MalePeopleController : ControllerBase
    {
        ICarLogic carlogic;
        IParkolasLogic parlogic;
        IHubContext<SignalRHub> hub;
        public MalePeopleController(ICarLogic carlogic, IParkolasLogic parlogic, IHubContext<SignalRHub> hub)
        {
            this.carlogic = carlogic;
            this.parlogic = parlogic;
            this.hub = hub;
        }
        //noncrud2
        [HttpGet]
        public IEnumerable<MalePeople> MalePeople()
        {
            return parlogic.MalePeopleHaveParking();
        }
    }
}
