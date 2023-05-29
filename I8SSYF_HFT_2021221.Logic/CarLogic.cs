using I8SSYF_HFT_2021221.Models;
using I8SSYF_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Logic
{
    public class CarLogic : ICarLogic
    {
        ICarRepository repo;

        public CarLogic(ICarRepository repo)
        {
            this.repo = repo;
        }

        public double AveragePrice()
        {
            return repo.ReadAll().Average(y => y.Price);

        }

        //1
        public IEnumerable<KeyValuePair<string, double>> AveragePriceByModels()
        {
            return repo.ReadAll().AsEnumerable().GroupBy(x => x.Model.Shape).Select(x => new KeyValuePair<string, double>(x.Key, x.Average(y => y.Price)));
        }

        //2
        public IEnumerable<KeyValuePair<string, double>> CylindersByDescending()
        {
            return repo.ReadAll().AsEnumerable().OrderByDescending(x => x.Engine.NumOfCylinders).Select(x => new KeyValuePair<string, double>(x.Name, x.Engine.NumOfCylinders));
        }

        //public int SedanCount()
        //{
        //    return repo.ReadAll().Select(x => x.Model).Where(x => x.Shape == "Sedan").Count();
        //}

        //public int PetrolCars()
        //{
        //    return repo.ReadAll().Select(x => x.Engine).Where(x => x.Fuel == "Petrol").Count();
        //}

        //3
        public IEnumerable<KeyValuePair<string, double>> AverageNumberOfCylindersByModels()
        {
            return repo.ReadAll().AsEnumerable().GroupBy(x => x.Model.Shape).Select(x => new KeyValuePair<string, double>(x.Key, x.Average(y => y.Engine.NumOfCylinders)));
        }

        //4
        public IEnumerable<KeyValuePair<string, double>> SumPriceByModels()
        {
            return repo.ReadAll().AsEnumerable().GroupBy(x => x.Model.Shape).Select(x => new KeyValuePair<string, double>(x.Key, x.Sum(y => y.Price)));
        }

        //5
        public IEnumerable<KeyValuePair<string, double>> CarCountByModels()
        {
            return repo.ReadAll().AsEnumerable().GroupBy(x => x.Model.Shape).Select(x => new KeyValuePair<string, double>(x.Key, x.Count()));
        }

        public void Create(Car car)
        {
            repo.Create(car);
        }

        public void Delete(int carId)
        {
            repo.Delete(carId);
        }

        public Car Read(int carId)
        {
            return repo.Read(carId) ?? throw new ArgumentException("Car with the specified id does not exists.");
        }

        public IQueryable<Car> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Car car)
        {
            repo.Update(car);
        }
    }
}
