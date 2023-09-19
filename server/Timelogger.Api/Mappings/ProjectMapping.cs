using System;
using Timelogger.Api.Dtos;
using Timelogger.Entities;

namespace Timelogger.Api.Mappings
{
    public static class ProjectMapping
    {
        public static ProjectGetResponseDto ToProjectGetResponseDto(this Project project)
        {

            return new ProjectGetResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                DeadLine = project.DeadLine,
                ClosedAt = project.ClosedAt,
                CreatedAt = project.CreatedAt,
            };
        }

        public static ProjectCreateResponseDto ToProjectCreateResponseDto(this Project project)
        {

            return new ProjectCreateResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                DeadLine = project.DeadLine,
                ClosedAt = project.ClosedAt,
                CreatedAt = project.CreatedAt,
            };
        }

        public static Project ToProject(this ProjectCreateRequestDto projectRequest)
        {

            return new Project
            {
                Id = Guid.NewGuid(),
                Name = projectRequest.Name,
                DeadLine = projectRequest.DeadLine,
            };
        }
    }
}