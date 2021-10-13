using System;

namespace News4Devs.Core.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException() : base() { }
        public DuplicateEmailException(string message) : base(message) { }
    }
}
