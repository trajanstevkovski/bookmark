using System;
using System.Runtime.Serialization;

namespace ReadLater.Services.Models.Exceptions
{
    [Serializable]
    public class BookmarkException : Exception
    {
        public BookmarkException()
        {
        }

        public BookmarkException(string message) : base(message)
        {
        }

        public BookmarkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BookmarkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
