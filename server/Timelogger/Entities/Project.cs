using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project : BaseEntity
	{
		public string Name { get; set; }
		public DateTime DeadLine { get; set; }
		public DateTime? ClosedAt { get; set; }

		public ICollection<Timelog> TimeLogs { get; private set; }

		public void AddTimeLog (Timelog timelog){
			if(TimeLogs == null)
				TimeLogs = new List<Timelog>();
			
			TimeLogs.Add(timelog);
		}
	}
}
