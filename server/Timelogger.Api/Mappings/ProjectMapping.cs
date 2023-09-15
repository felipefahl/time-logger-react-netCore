namespace Timelogger.Api.Mappings
{
    public static class ProjectMapping {
        public static ProjectGetResponseDto ToProjectGetResponseDto(this Project project){
            
            return new ProjectGetResponseDto {
                Id = project.Id,
                Name = project.Name,
                DeadLine = project.DeadLine,
                ClosedAt = project.ClosedAt,
                CreatedAt = project.CreatedAt,
            };
        }
    }
}