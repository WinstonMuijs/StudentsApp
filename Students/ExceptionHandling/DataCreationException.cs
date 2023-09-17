using System;
namespace Students.ExceptionHandling
{
    public class DataCreationException : Exception
    {
        public DataCreationException()
        {
        }

        public DataCreationException(string message) : base(message)
        {
        }

        public DataCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}

