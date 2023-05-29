using I8SSYF_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Logic
{
    public interface IModelLogic
    {
        void Create(Model model);
        IQueryable<Model> ReadAll();
        void Update(Model model);
        void Delete(int modelId);
        Model Read(int modelId);
    }
}
