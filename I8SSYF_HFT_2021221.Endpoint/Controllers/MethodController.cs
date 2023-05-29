using I8SSYF_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace I8SSYF_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MethodController : ControllerBase
    {
        ICarLogic carLogic;

        public MethodController(ICarLogic carLogic)
        {
            this.carLogic = carLogic;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AverageNumberOfCylindersByModels()
        {
            return this.carLogic.AverageNumberOfCylindersByModels();
        }

        [HttpGet]
        public double AveragePrice()
        {
            return this.carLogic.AveragePrice();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AveragePriceByModels()
        {
            return this.carLogic.AveragePriceByModels();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> CarCountByModels()
        {
            return this.carLogic.CarCountByModels();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> SumPriceByModels()
        {
            return this.carLogic.SumPriceByModels();
        }

        [HttpGet]

        public IEnumerable<KeyValuePair<string, double>> CylindersByDescending()
        {
            return this.carLogic.CylindersByDescending();
        }

        //[HttpGet]
        //public int PetrolCars()
        //{
        //    return this.carLogic.PetrolCars();
        //}

        //[HttpGet]
        //public int SedanCount()
        //{
        //    return this.carLogic.SedanCount();
        //}
    }
}
