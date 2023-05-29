using I8SSYF_HFT_2021221.Models;
using I8SSYF_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Logic
{
    public class EngineLogic : IEngineLogic
    {
        IEngineRepository repo;

        public EngineLogic(IEngineRepository repo)
        {
            this.repo = repo;
        }

        public void Create(Engine engine)
        {
            repo.Create(engine);
        }

        public void Delete(int engineId)
        {
            repo.Delete(engineId);
        }

        public Engine Read(int engineId)
        {
            return repo.Read(engineId);
        }

        public IQueryable<Engine> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Engine engine)
        {
            repo.Update(engine);
        }
    }
}
