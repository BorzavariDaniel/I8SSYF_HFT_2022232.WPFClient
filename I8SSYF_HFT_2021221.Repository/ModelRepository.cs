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
    public class ModelRepository : IModelRepository
    {
        CarDbContext db;

        public ModelRepository(CarDbContext db)
        {
            this.db = db;
        }

        public void Create(Model model)
        {
            db.Models.Add(model);
            db.SaveChanges();
        }

        public void Delete(int modelId)
        {
            var modelToDelete = Read(modelId);
            db.Models.Remove(modelToDelete);
            db.SaveChanges();
        }

        public Model Read(int modelId)
        {
            return db.Models
                .FirstOrDefault(t => t.ModelId == modelId);
        }

        public IQueryable<Model> ReadAll()
        {
            return db.Models;
        }

        public void Update(Model model)
        {
            var old = Read(model.ModelId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(model));
                }
            }
            db.SaveChanges();
        }
    }
}
