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
    public class ModelController : ControllerBase
    {
        IModelLogic modelLogic;
        IHubContext<SignalRHub> hub;

        public ModelController(IModelLogic modelLogic, IHubContext<SignalRHub> hub)
        {
            this.modelLogic = modelLogic;
            this.hub = hub;
        }

        // GET: /model
        [HttpGet]
        public IEnumerable<Model> Get()
        {
            return modelLogic.ReadAll();
        }

        // GET /model/id
        [HttpGet("{id}")]
        public Model Get(int id)
        {
            return modelLogic.Read(id);
        }

        // POST /model
        [HttpPost]
        public void Post([FromBody] Model value)
        {
            modelLogic.Create(value);
            this.hub.Clients.All.SendAsync("ModelCreated", value);
        }

        // PUT /model/id
        [HttpPut]
        public void Put(int id, [FromBody] Model value)
        {
            modelLogic.Update(value);
            this.hub.Clients.All.SendAsync("ModelUpdated", value);
        }

        // DELETE /model/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var modelToDelete = this.modelLogic.Read(id);
            modelLogic.Delete(id);
            this.hub.Clients.All.SendAsync("ModelDeleted", modelToDelete);
        }
    }
}
