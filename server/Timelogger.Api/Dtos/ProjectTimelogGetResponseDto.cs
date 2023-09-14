using System;

namespace Timelogger.Api.Dtos
{
	public class ProjectTimelogGetResponseDto
	{
		public Guid Id { get; set; }
		public Guid ProjectId { get; set; }
		public long DurationMinutes { get; set; }
		public string Note { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}