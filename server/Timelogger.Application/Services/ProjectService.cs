using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timelogger.Entities;
using Timelogger.Exceptions;
using Timelogger.Interfaces.Repositories;
using Timelogger.Interfaces.Services;

namespace Timelogger.Application.Services
{
    public class ProjectService : IProjectService {

        private readonly IProjectRepository _projectRepository;
        private readonly ITimelogRepository _timelogRepository;

        public ProjectService(IProjectRepository projectRepository, ITimelogRepository timelogRepository)
        {
            _projectRepository = projectRepository;
            _timelogRepository = timelogRepository;
        }

        public async Task<Project> CreateAsync(Project project){
            return await _projectRepository.CreateAsync(project);
        }

        public async Task<IList<Timelog>> GetProjectTimeLogListAsync(Guid id){
            var project = await _projectRepository.GetAsync(id);

            if(project == null)
                throw new NotFoundException("project", $"{id}");

            return project.TimeLogs.ToList();
        }

        public async Task<Timelog> InsertProjectTimeLogAsync(Guid id, Timelog timelog, bool projectFinished) {
            var project = await _projectRepository.GetAsync(id);

            if(project == null)
                throw new NotFoundException("project", $"{id}");

            if (project.ClosedAt.HasValue)
                throw new BadRequestException($"project {id} is closed");

            await _timelogRepository.CreateAsync(timelog);

            if (projectFinished)
                project.ClosedAt = DateTime.UtcNow;

            project.AddTimeLog(timelog);

            await _projectRepository.UpdateAsync(project);

            await _projectRepository.CommitAsync();

            timelog.Project = project;
            return timelog;
        }

        public async Task<IReadOnlyList<Project>> ListProjectAsync(bool orderByDeadline = false, bool onlyActives = false){
            var projects = Enumerable.Empty<Project>();

            if(onlyActives){
                Expression<Func<Project, bool>> onlyActivesExtract = p => !p.ClosedAt.HasValue;
                projects = await _projectRepository.GetAllByCriteriaAync(onlyActivesExtract);
            }
            else    
                projects = await _projectRepository.GetAllAsync();

            projects = orderByDeadline ? projects.OrderBy(x => x.DeadLine).ToList() : projects.OrderByDescending(x => x.CreatedAt).ToList();  

            return projects.ToList().AsReadOnly();
        }
    }
}