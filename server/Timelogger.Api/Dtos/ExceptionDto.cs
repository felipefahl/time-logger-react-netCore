using System.Collections.Generic;

namespace Timelogger.Api.Dtos
{
    public class ExceptionDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string TraceId { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
