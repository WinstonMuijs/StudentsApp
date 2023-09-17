using System;
namespace Students.ExceptionHandling
{
    public class DataDeleteException : Exception
    {
        public DataDeleteException()
        {
        }

        public DataDeleteException(string message) : base(message)
        {
        }

        public DataDeleteException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

