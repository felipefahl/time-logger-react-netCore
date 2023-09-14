using System.Threading.Tasks;

namespace Timelogger.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();       
    }
}