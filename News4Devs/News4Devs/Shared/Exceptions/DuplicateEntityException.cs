using System;
using System.Runtime.Serialization;

namespace News4Devs.Shared.Exceptions
{
    [Serializable]
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException() : base() { }

        public DuplicateEntityException(string message) : base(message) { }

        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException) { }

        protected DuplicateEntityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
