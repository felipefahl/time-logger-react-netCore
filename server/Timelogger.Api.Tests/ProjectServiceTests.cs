namespace Timelogger.Api.Tests
{
    public class ProjectServiceTests()
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITimelogRepository _timelogRepository;

        private static ValidTimelogToInsert(Guid projectId) => new Timelog
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Note = "Test InsertProjectTimeLogAsync",
                DurationMinutes = 30,
            };

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
            };

            new Project
            {
                Id = Guid.Parse("af4a717b-fa7d-44cb-80a0-9fb59c14bdb8"),
                Name = "Family business",
                DeadLine = DateTime.UtcNow.AddDays(15),
            };

            new Project
            {
                Id = Guid.Parse("b9bfedfc-bc35-4c96-86b2-845df714d1d4"),
                Name = "free lancer",
                DeadLine = DateTime.UtcNow.AddDays(35),
            };
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
            };

            new Timelog
            {
                Id = Guid.Parse("abd92c5e-8d1e-45fb-9c8e-5bb31f4d9b8a"),
                ProjectId = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Note = "Week 3",
                DurationMinutes = 33,
            };

            new Timelog
            {
                Id = Guid.Parse("401b989f-e775-4575-8833-3c07c1d2f3c2"),
                ProjectId = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Note = "Week 4",
                DurationMinutes = 30,
            };
        };

        public ProjectServiceTests()
        {
            _projectRepository = Substitute.For<IProjectRepository>();
            _timelogRepository = Substitute.For<ITimelogRepository>();
        }

        [Fact]
        public async Task ListProjectAsync_ShouldReply_ListOfProjects()
        {
            var defaultProjectList = SeedProjects.OrderByDescending(x => x.CreatedAt).ToList();
            
            _projectRepository.GetAllAsync().Returns(SeedProjects);
            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            var actual = await sut.ListProjectAsync();

            actual.Should().NotBeEmpty()
                .And.ContainEquivalentOf(defaultProjectList)
                .And.ContainItemsAssignableTo<Project>();

            _projectRepository.DidNotReceive().GetAllByCriteriaAync(Arg.Any<Expression<Func<Project, bool>>>());
        }

        [Fact]
        public async Task ListProjectAsync_WhenSortByDeadline_ShouldReply_ListOfProjectsSorted()
        {
            var sortedList = SeedProjects.OrderBy(x => x.DeadLine).ToList();
            _projectRepository.GetAllAsync().Returns(SeedProjects);
            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            var actual = await sut.ListProjectAsync(orderByDeadline: true);

            actual.Should().NotBeEmpty()
                .And.ContainEquivalentOf(sortedList)
                .And.ContainItemsAssignableTo<Project>();

            _projectRepository.DidNotReceive().GetAllByCriteriaAync(Arg.Any<Expression<Func<Project, bool>>>());
        }

        [Fact]
        public async Task ListProjectAsync_WhenFilterByActives_ShouldReply_ListOfProjectsSorted()
        {
            var filteredList = SeedProjects
                .Where(x => x.ClosedAt.HasValue)                
                .ToList();
            _projectRepository.GetAllByCriteriaAync(Arg.Any<Expression<Func<Project, bool>>>()).Returns(filteredList);
            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            var actual = await sut.ListProjectAsync(onlyActives: true);

            actual.Should().NotBeEmpty()
                .And.ContainEquivalentOf(filteredList.OrderByDescending(x => x.CreatedAt))
                .And.ContainItemsAssignableTo<Project>();

            _projectRepository.DidNotReceive().GetAllByCriteriaAync(Arg.Any<Expression<Func<Project, bool>>>());
        }

        [Fact]
        public async Task GetProjectTimeLogListAsync_ShouldReply_ListOfTimeLogs()
        {
            var project = SeedTimelogs.FirstOrDefault();
            var projectId = project.Id;
            var timelogs = SeedTimelogs.Where(x => x.ProjectId == projectId).ToList();
            timelogs.ForEach(x => {
                project.AddTimeLog(x);
            });
            
            _projectRepository.GetAsync(projectId).Returns(project);

            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            var actual = await sut.GetProjectTimeLogListAsync(projectId);

            actual.Should().NotBeEmpty()
                .And.ContainEquivalentOf(timelogs)
                .And.ContainItemsAssignableTo<Timelog>();
        }

        [Fact]
        public async Task GetProjectTimeLogListAsync_WhenNotExists_ShouldThrow_NotFoundException()
        {
            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            Action action = async () => await sut.GetProjectTimeLogListAsync(Guid.NewGuid());

            action.Should()
                .Throw<NotFoundException>();
        }

        [Fact]
        public async Task InsertProjectTimeLogAsync_ShouldReply_InsertedTimeLog()
        {
            var project = SeedTimelogs.Where(x => !x.ClosedAt.HasValue).FirstOrDefault();
            var projectId = project.Id;
            var timelog = ValidTimelogToInsert(projectId);
            
            _projectRepository.GetAsync(projectId).Returns(project);

            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            var actual = await sut.InsertProjectTimeLogAsync(projectId, timelog, projectFinished: false);

            actual.Should().NotBeEmpty()
                .And.BeOfType<Timelog>();

            actual.Project.ClosedAt.Should().BeEmpty();
            await _timelogRepository.Received().CreateAsync(Arg.Any<Timelog>());
            await _projectRepository.Received().UpdateAsync(Arg.Any<Project>());
            await _projectRepository.Received().CommitAsync();
        }

        [Fact]
        public async Task InsertProjectTimeLogAsync_WhenHasToFinishesProject_ShouldReply_InsertedTimeLogWithProjectClosed()
        {
            var project = SeedTimelogs.Where(x => !x.ClosedAt.HasValue).FirstOrDefault();
            var projectId = project.Id;
            var timelog = ValidTimelogToInsert(projectId);
            
            _projectRepository.GetAsync(projectId).Returns(project);

            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            var actual = await sut.InsertProjectTimeLogAsync(projectId, timelog, projectFinished: true);

            actual.Should().NotBeEmpty()
                .And.BeOfType<Timelog>();

            actual.Project.ClosedAt.Should().BeNotEmpty();
            await _timelogRepository.Received().CreateAsync(Arg.Any<Timelog>());
            await _projectRepository.Received().UpdateAsync(Arg.Any<Project>());
            await _projectRepository.Received().CommitAsync();
        }

        [Fact]
        public async Task InsertProjectTimeLogAsync_WhenProjectWasFinishedBefore_ShouldThrow_BadRequestException()
        {
            var project = SeedTimelogs.Where(x => x.ClosedAt.HasValue).FirstOrDefault();
            var projectId = project.Id;
            var timelog = ValidTimelogToInsert(projectId);
            
            _projectRepository.GetAsync(projectId).Returns(project);

            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            Action action = async () => await sut.InsertProjectTimeLogAsync(projectId, timelog, projectFinished: false);

            action.Should()
                .Throw<BadRequestException>();

            await _projectRepository.DidNotReceive().CommitAsync();
        }

        [Fact]
        public async Task InsertProjectTimeLogAsync_WhenProjectNotFound_ShouldThrow_NotFoundException()
        {
            var project = SeedTimelogs.Where(x => x.ClosedAt.HasValue).FirstOrDefault();
            var projectId = project.Id;
            var timelog = ValidTimelogToInsert(projectId);
            
            _projectRepository.GetAsync(projectId).Returns(project);

            ProjectService sut = new ProjectService(_projectRepository, _timelogRepository);

            Action action = async () => await sut.InsertProjectTimeLogAsync(projectId, timelog, projectFinished: false);

            action.Should()
                .Throw<NotFoundException>();

            await _projectRepository.DidNotReceive().CommitAsync();
        }
    }    
}