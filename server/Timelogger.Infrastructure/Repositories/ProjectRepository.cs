using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timelogger.Entities;
using Timelogger.Interfaces.Repositories;

namespace Timelogger.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApiContext _context;

        public ProjectRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Project> GetAsync(Guid id)
        {
            return await _context.Projects.Include(x => x.TimeLogs).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>> GetAllByCriteriaAync(Expression<Func<Project, bool>> expression)
        {
            return await Task.FromResult(_context.Projects.Where(expression).AsEnumerable());
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await Task.FromResult(_context.Projects.AsEnumerable());
        }

        public Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            return Task.CompletedTask;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }
    }
}