using System;

namespace Timelogger.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string item, string itemId) : base($"{item} {itemId} not found")
        {

        }
    }
}
