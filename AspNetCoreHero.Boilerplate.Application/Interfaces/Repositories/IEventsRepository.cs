using AspNetCoreHero.Boilerplate.Domain.Entities.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IEventsRepository
    {
        IQueryable<MiningRaceEvents> Events { get; }

        Task<List<MiningRaceEvents>> GetListAsync();

        Task<MiningRaceEvents> GetByIdAsync(Guid eventsId);

        Task<Guid> InsertAsync(MiningRaceEvents events);

        Task UpdateAsync(MiningRaceEvents events);

        Task DeleteAsync(MiningRaceEvents events);
    }
}
