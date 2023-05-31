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
    public class CarController : ControllerBase
    {
        ICarLogic carLogic;
        IHubContext<SignalRHub> hub;

        public CarController(ICarLogic carLogic, IHubContext<SignalRHub> hub)
        {
            this.carLogic = carLogic;
            this.hub = hub;
        }

        // GET: /car
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return carLogic.ReadAll();
        }

        // GET /car/id
        [HttpGet("{id}")]
        public Car Get(int id)
        {
            return carLogic.Read(id);
        }

        // POST /car
        [HttpPost]
        public void Post([FromBody] Car value)
        {
            carLogic.Create(value);
            this.hub.Clients.All.SendAsync("CarCreated", value);
        }

        // PUT /car/id
        [HttpPut]
        public void Put(int id, [FromBody] Car value)
        {
            carLogic.Update(value);
            this.hub.Clients.All.SendAsync("CarUpdated", value);
        }

        // DELETE /car/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var carToDelete = this.carLogic.Read(id);
            carLogic.Delete(id);
            this.hub.Clients.All.SendAsync("CarDeleted", carToDelete);
        }
    }
}
