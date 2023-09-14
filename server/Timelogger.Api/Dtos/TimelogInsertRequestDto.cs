namespace Timelogger.Api.Dtos
{
	public class TimelogInsertRequestDto
	{
		public long DurationMinutes { get; set; }
		public string Note { get; set; }
		public bool ProjectFinished { get; set; }
	}
}