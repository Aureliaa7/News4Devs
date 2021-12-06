using System;
using System.Runtime.Serialization;

namespace News4Devs.Shared.Exceptions
{
    [Serializable]
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException() : base() { }
      
        public DuplicateEmailException(string message) : base(message) { }

        public DuplicateEmailException(string message, Exception innerException) : base(message, innerException) { }

        // Without this constructor, deserialization will fail
        protected DuplicateEmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
