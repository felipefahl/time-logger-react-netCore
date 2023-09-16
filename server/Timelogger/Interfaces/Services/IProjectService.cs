using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Interfaces.Services
{
    public interface IProjectService
    {
        Task<IList<Timelog>> GetProjectTimeLogListAsync(Guid id);
        Task<Timelog> InsertProjectTimeLogAsync(Guid id, Timelog timelog, bool projectFinished);
        Task<IReadOnlyList<Project>> ListProjectAsync(bool orderByDeadline = false, bool onlyActives = false);
    }
}