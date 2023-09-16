using System.Threading.Tasks;
using Timelogger.Entities;
using Timelogger.Interfaces.Repositories;

namespace Timelogger.Infrastructure.Repositories
{
    public class TimelogRepository : ITimelogRepository
    {
        private readonly ApiContext _context;

        public TimelogRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Timelog timelog)
        {
            await _context.Timelogs.AddAsync(timelog);
        }
    }
}
