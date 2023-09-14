using System;

namespace Timelogger.Api.Dtos
{
	public class TimelogInsertResponseDto
	{
		public Guid Id { get; set; }
		public Guid ProjectId { get; set; }
		public long DurationMinutes { get; set; }
		public string Note { get; set; }
		public bool ProjectFinished { get; set; }
	}
}