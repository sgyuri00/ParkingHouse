using Microsoft.AspNetCore.Mvc;
using Parkolo.Logic;
using Parkolo.Data;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.SignalR;
using Parkolo.Endpoint.Services;

namespace Parkolo.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SzemelyController : ControllerBase
    {
        IParkolasLogic logic;
        IHubContext<SignalRHub> hub;
        public SzemelyController(IParkolasLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Szemely> ReadAll()
        {
            return this.logic.GetAllSzemely();
        }

        [HttpGet("{id}")]
        public Szemely Read(int id)
        {
            return this.logic.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] Szemely person)
        {
            this.logic.AddPerson(person.SzemelyId, person.Nev, person.Nem, person.SzuletesiIdo);
            this.hub.Clients.All.SendAsync("SzemelyCreated", person);
        }

        [HttpPut]
        public void Put(Szemely person)
        {
            ;
            this.logic.ChangeName(person.SzemelyId, person.Nev);
            this.hub.Clients.All.SendAsync("SzemelyUpdated", person);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var person = this.logic.GetById(id);
            this.logic.DeletePerson(id);
            this.hub.Clients.All.SendAsync("SzemelyDeleted", person);
        }
    }
}
