using I8SSYF_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Repository
{
    public interface IEngineRepository
    {
        void Create(Engine engine);
        void Delete(int engineId);
        Engine Read(int engineId);
        IQueryable<Engine> ReadAll();
        void Update(Engine engine);
    }
}
