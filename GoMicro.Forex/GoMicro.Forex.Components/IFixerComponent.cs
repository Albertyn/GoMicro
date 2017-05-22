using GoMicro.Forex.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoMicro.Forex.Components
{
    public interface IFixerComponent
    {
        Task<Fixer> Get(string @base);

        Task Add(Fixer F);

        Task<List<Fixer>> ListFixesByDate(DateTime Date);
        Task<Fixer> GetFixerLatest(string Base);


        //void Update(Fixer F);
        //void Delete(Fixer F);
    }
}
