using System;

namespace Timelogger.Api.Dtos
{
	public class ProjectGetResponseDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime DeadLine { get; set; }
		public DateTime? ClosedAt { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}