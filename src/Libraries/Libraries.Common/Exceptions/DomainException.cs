using System;

namespace Libraries.Common.Exceptions;

public class DomainException : Exception
{
    public DomainException() { }
    public DomainException(string message) : base(message) { }
}
