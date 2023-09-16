using System;

namespace Timelogger.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; }        
    }
}