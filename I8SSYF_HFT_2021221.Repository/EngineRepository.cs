using I8SSYF_HFT_2021221.Data;
using I8SSYF_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Repository
{
    public class EngineRepository : IEngineRepository
    {
        CarDbContext db;

        public EngineRepository(CarDbContext db)
        {
            this.db = db;
        }

        public void Create(Engine engine)
        {
            db.Engines.Add(engine);
            db.SaveChanges();
        }

        public void Delete(int engineId)
        {
            var engineToDelete = Read(engineId);
            db.Engines.Remove(engineToDelete);
            db.SaveChanges();
        }

        public Engine Read(int engineId)
        {
            return db.Engines
                .FirstOrDefault(t => t.EngineId == engineId);
        }

        public IQueryable<Engine> ReadAll()
        {
            return db.Engines;
        }

        public void Update(Engine engine)
        {
            var old = Read(engine.EngineId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(engine));
                }
            }
            db.SaveChanges();
        }
    }
}