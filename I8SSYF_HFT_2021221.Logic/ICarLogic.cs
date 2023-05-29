using I8SSYF_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Logic
{
    public interface ICarLogic
    {
        void Create(Car car);
        IQueryable<Car> ReadAll();
        void Update(Car car);
        void Delete(int carId);
        Car Read(int carId);

        double AveragePrice();

        IEnumerable<KeyValuePair<string, double>>
            AveragePriceByModels();

        IEnumerable<KeyValuePair<string, double>>
            CylindersByDescending();

        //int SedanCount();

        //int PetrolCars();

        IEnumerable<KeyValuePair<string, double>>
            AverageNumberOfCylindersByModels();

        IEnumerable<KeyValuePair<string, double>>
            SumPriceByModels();

        IEnumerable<KeyValuePair<string, double>>
            CarCountByModels();

    }
}
