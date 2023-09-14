using System;

namespace Timelogger.Entities
{
	public class Timelog : BaseEntity
	{
		public Guid ProjectId { get; set; }
		public long DurationMinutes { get; set; }
		public string Note { get; set; }
		
		public Project Project { get; set; }
	}
}