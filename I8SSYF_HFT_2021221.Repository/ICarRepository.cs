using I8SSYF_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Repository
{
    public interface ICarRepository
    {
        void Create(Car car);
        void Delete(int carId);
        Car Read(int carId);
        IQueryable<Car> ReadAll();
        void Update(Car car);
    }
}
