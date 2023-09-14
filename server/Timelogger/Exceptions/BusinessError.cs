namespace Timelogger.Exceptions
{
    public class BusinessError
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public BusinessError(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
