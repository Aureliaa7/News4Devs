using System;
using System.Runtime.Serialization;

namespace News4Devs.Shared.Exceptions
{ 
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base() { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
