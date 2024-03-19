namespace Shared.Infrastructures.Extensions
{
    public class CustomException : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception innerException) : base(message, innerException) { }
        public List<(string message, string param)> ErrorMessages { get; }

        public CustomException(string message, List<(string message, string param)> errorMessages) : base(message)
        {
            ErrorMessages = errorMessages;
        }
    }
}
