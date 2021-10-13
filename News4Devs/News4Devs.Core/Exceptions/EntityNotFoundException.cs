using System;

namespace News4Devs.Core.Exceptions
{ 
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base() { }
        public EntityNotFoundException(string message) : base(message) { }
    }
}
