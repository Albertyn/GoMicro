using GoMicro.Forex.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoMicro.Forex.Components.Repositories
{
    public interface IFixerRepository : IRepository<Fixer>
    {
        Task<Fixer> GetLatestFixer(string Base);
    }
}
