using I8SSYF_HFT_2021221.Data;
using I8SSYF_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Repository
{
    public class CarRepository : ICarRepository
    {
        CarDbContext db;

        public CarRepository(CarDbContext db)
        {
            this.db = db;
        }

        public void Create(Car car)
        {
            db.Cars.Add(car);
            db.SaveChanges();
        }

        public void Delete(int carId)
        {
            var carToDelete = Read(carId);
            db.Cars.Remove(carToDelete);
            db.SaveChanges();
        }

        public Car Read(int carId)
        {
            return db.Cars
                .FirstOrDefault(t => t.Id == carId);
        }

        public IQueryable<Car> ReadAll()
        {
            return db.Cars;
        }

        public void Update(Car car)
        {
            var old = Read(car.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(car));
                }
            }
            db.SaveChanges();
        }
    }
}
