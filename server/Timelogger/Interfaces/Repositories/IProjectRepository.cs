using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Interfaces.Repositories
{
    public interface IProjectRepository : IUnitOfWork
    {
        Task<Project> GetAsync(Guid id);
        Task<IEnumerable<Project>> GetAllByCriteriaAync(Expression<Func<Project, bool>> expression);
        Task<IEnumerable<Project>> GetAllAsync();
        Task UpdateAsync(Project project);
    }
}