using GoMicro.Forex.Components.Repositories;
using GoMicro.Forex.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace GoMicro.Forex.Components
{
    public class FixerDbComponent : IFixerDbComponent
    {
        IFixerRepository _FixerRepository;
        public FixerDbComponent(IFixerRepository FixerRepository)
        {
            _FixerRepository = FixerRepository;
        }
        public async Task<Fixer> Get(string IsoAlpha3Code)
        {
            return await _FixerRepository.Get(IsoAlpha3Code);
        }
        public async Task<List<Fixer>> ListFixesByDate(DateTime Date)
        {
            FilterDefinition<Fixer> filter = Builders<Fixer>.Filter.Eq(x => x.date, Date.ToString("yyyy-MM-dd"));            
            return await _FixerRepository.All(filter);
        }

        public async Task<Fixer> GetFixerLatest(string Base)
        {
            return await _FixerRepository.GetLatestFixer(Base);
        }
        public async Task Add(Fixer F)
        {
            await _FixerRepository.Add(F);
        }

    }
}
