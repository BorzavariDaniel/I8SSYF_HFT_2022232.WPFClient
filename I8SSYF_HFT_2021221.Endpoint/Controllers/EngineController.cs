using I8SSYF_HFT_2021221.Endpoint.Services;
using I8SSYF_HFT_2021221.Logic;
using I8SSYF_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace I8SSYF_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        IEngineLogic engineLogic;
        IHubContext<SignalRHub> hub;

        public EngineController(IEngineLogic engineLogic, IHubContext<SignalRHub> hub)
        {
            this.engineLogic = engineLogic;
            this.hub = hub;
        }

        // GET: /engine
        [HttpGet]
        public IEnumerable<Engine> Get()
        {
            return engineLogic.ReadAll();
        }

        // GET /engine/id
        [HttpGet("{id}")]
        public Engine Get(int id)
        {
            return engineLogic.Read(id);
        }

        // POST /engine
        [HttpPost]
        public void Post([FromBody] Engine value)
        {
            engineLogic.Create(value);
            this.hub.Clients.All.SendAsync("EngineCreated", value);
        }

        // PUT /engine/id
        [HttpPut]
        public void Put(int id, [FromBody] Engine value)
        {
            engineLogic.Update(value);
            this.hub.Clients.All.SendAsync("EngineUpdated", value);
        }

        // DELETE /engine/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var engineToDelete = this.engineLogic.Read(id);
            engineLogic.Delete(id);
            this.hub.Clients.All.SendAsync("EngineDeleted", engineToDelete);
        }
    }
}
