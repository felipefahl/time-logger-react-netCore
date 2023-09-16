using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Interfaces.Repositories
{
    public interface ITimelogRepository
    {
        Task CreateAsync(Timelog timelog);
    }
}
