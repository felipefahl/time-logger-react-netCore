using System;

namespace Timelogger.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreatedAt { get; set; }        
    }
}