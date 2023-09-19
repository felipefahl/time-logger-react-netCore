using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timelogger.Api.Controllers;
using Timelogger.Api.Dtos;
using Timelogger.Api.Mappings;
using Timelogger.Entities;
using Timelogger.Interfaces.Services;
using Xunit;

namespace Timelogger.Api.Tests
{
    public class ProjectsControllerTests
    {
        private readonly IProjectService _projectService;

        private static List<Project> SeedProjects => new List<Project>{
            new Project
            {
                Id = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Name = "e-conomic Interview",
                DeadLine = DateTime.UtcNow.AddDays(5),
                ClosedAt = DateTime.UtcNow.AddDays(-1)
            },

            new Project
            {
                Id = Guid.Parse("794fd433-9347-4ce4-a999-14d115bd0dbe"),
                Name = "Personal Data",
                DeadLine = DateTime.UtcNow.AddDays(-9),
            },

            new Project
            {
                Id = Guid.Parse("af4a717b-fa7d-44cb-80a0-9fb59c14bdb8"),
                Name = "Family business",
                DeadLine = DateTime.UtcNow.AddDays(15),
            },

            new Project
            {
                Id = Guid.Parse("b9bfedfc-bc35-4c96-86b2-845df714d1d4"),
                Name = "free lancer",
                DeadLine = DateTime.UtcNow.AddDays(35),
            },
        };

        private static List<Timelog> SeedTimelogs => new List<Timelog>{
            new Timelog
            {
                Id = Guid.Parse("b04d843c-0358-423b-a9b5-78addfd43baa"),
                ProjectId = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Note = "Week 1",
                DurationMinutes = 55,
            },

            new Timelog
            {
                Id = Guid.Parse("1a7e84d4-1bf2-41e8-994f-08e3fe04452a"),
                ProjectId = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Note = "Week 2",
                DurationMinutes = 48,
            },

            new Timelog
            {
                Id = Guid.Parse("abd92c5e-8d1e-45fb-9c8e-5bb31f4d9b8a"),
                ProjectId = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Note = "Week 3",
                DurationMinutes = 33,
            },

            new Timelog
            {
                Id = Guid.Parse("401b989f-e775-4575-8833-3c07c1d2f3c2"),
                ProjectId = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Note = "Week 4",
                DurationMinutes = 30,
            },
        };

        public ProjectsControllerTests()
        {
            _projectService = Substitute.For<IProjectService>();
        }

        [Fact]
        public async Task GetList_ShouldReply_ListOfProjects()
        {
            _projectService.ListProjectAsync(sortByDeadline: false, onlyActives: false).Returns(SeedProjects);
            ProjectsController sut = new ProjectsController(_projectService);

            var result = await sut.GetList();
            var okResult = result as OkObjectResult;
            var actual = okResult.Value as List<ProjectGetResponseDto>;

            actual.Should().NotBeEmpty()
                .And.ContainItemsAssignableTo<ProjectGetResponseDto>();
        }

        [Fact]
        public async Task GetList_WhenSortByDeadline_ShouldReply_ListOfProjectsSorted()
        {
            var sortedList = SeedProjects.OrderBy(x => x.DeadLine).ToList();
            _projectService.ListProjectAsync(sortByDeadline: true, onlyActives: false).Returns(sortedList);
            ProjectsController sut = new ProjectsController(_projectService);

            var result = await sut.GetList(sortByDeadline: true);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value as List<ProjectGetResponseDto>;

            actual.Should().NotBeEmpty()
                .And.ContainItemsAssignableTo<ProjectGetResponseDto>();
        }

        [Fact]
        public async Task GetList_WhenFilterByActives_ShouldReply_ListOfProjectsSorted()
        {
            var filteredList = SeedProjects.Where(x => x.ClosedAt.HasValue).ToList();
            _projectService.ListProjectAsync(sortByDeadline: false, onlyActives: true).Returns(filteredList);
            ProjectsController sut = new ProjectsController(_projectService);

            var result = await sut.GetList(onlyActives: true);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value as List<ProjectGetResponseDto>;

            actual.Should().NotBeEmpty()
                .And.ContainItemsAssignableTo<ProjectGetResponseDto>();
        }

        [Fact]
        public async Task GetTimelogs_ShouldReply_ListOfTimeLogProject()
        {
            var projectId = SeedProjects.FirstOrDefault().Id;
            var timelogs = SeedTimelogs.Where(x => x.ProjectId == projectId).ToList();

            _projectService.GetProjectTimeLogListAsync(projectId).Returns(timelogs);
            ProjectsController sut = new ProjectsController(_projectService);

            var result = await sut.GetTimelogs(projectId);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value as List<ProjectTimelogGetResponseDto>;

            actual.Should().NotBeEmpty()
                .And.ContainItemsAssignableTo<ProjectTimelogGetResponseDto>();
        }

        [Fact]
        public async Task InsertTimelog_ShouldReply_InsertedProjectTimeLog()
        {
            var project = SeedProjects.FirstOrDefault();
            var projectId = project.Id;
            var timelog = SeedTimelogs.Where(x => x.ProjectId == projectId).FirstOrDefault();

            var insertRequest = new TimelogInsertRequestDto
            {
                DurationMinutes = 30,
                Note = "Week develepment",
                ProjectFinished = false,
            };

            _projectService.InsertProjectTimeLogAsync(projectId,
                    Arg.Is<Timelog>(x => x.DurationMinutes == insertRequest.DurationMinutes && x.Note == insertRequest.Note),
                    insertRequest.ProjectFinished)
                .Returns(timelog);
            ProjectsController sut = new ProjectsController(_projectService);

            var result = await sut.InsertTimelog(projectId, insertRequest);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value as TimelogInsertResponseDto;

            actual.Should().NotBeNull()
                .And.BeOfType<TimelogInsertResponseDto>();
        }

        [Fact]
        public async Task InsertTimelog_WhenInvalidDurationMinutes_ShouldThrow_ValidationException()
        {
            var project = SeedTimelogs.FirstOrDefault();
            var projectId = project.Id;
            var timelog = SeedTimelogs.Where(x => x.ProjectId == projectId).FirstOrDefault();

            var insertRequest = new TimelogInsertRequestDto
            {
                DurationMinutes = 29,
                Note = "Week develepment",
                ProjectFinished = false,
            };

            _projectService.InsertProjectTimeLogAsync(projectId,
                    Arg.Is<Timelog>(x => x.DurationMinutes == insertRequest.DurationMinutes && x.Note == insertRequest.Note),
                    insertRequest.ProjectFinished)
                .Returns(timelog);
            ProjectsController sut = new ProjectsController(_projectService);

            Func<Task> action = async () => await sut.InsertTimelog(projectId, insertRequest);

            await action.Should()
                .ThrowAsync<ValidationException>()
                .WithMessage("*Must be greater then or equal to 30*");
        }

        [Fact]
        public async Task Create_ShouldReply_InsertedProject()
        {
            var insertRequest = new ProjectCreateRequestDto
            {
                DeadLine = DateTime.Today,
                Name = "Project UnitTest",
            };
            var project = insertRequest.ToProject();

            _projectService.CreateAsync(Arg.Any<Project>()).Returns(project);
            ProjectsController sut = new ProjectsController(_projectService);

            var result = await sut.Create(insertRequest);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value as ProjectCreateResponseDto;

            actual.Should().NotBeNull()
                .And.BeOfType<ProjectCreateResponseDto>();
        }

        [Fact]
        public async Task Create_WhenInvalidDeadLine_ShouldThrow_ValidationException()
        {
            var insertRequest = new ProjectCreateRequestDto
            {
                DeadLine = DateTime.Today.AddDays(-1),
                Name = "Project UnitTest",
            };
            var project = insertRequest.ToProject();

            _projectService.CreateAsync(Arg.Any<Project>()).Returns(project);
            ProjectsController sut = new ProjectsController(_projectService);

            Func<Task> action = async () => await sut.Create(insertRequest);

            await action.Should()
               .ThrowAsync<ValidationException>()
               .WithMessage("*Must be greater then or equal to Today*");
        }
    }
}
