using System;

namespace Libraries.Common.Exceptions;

public class InvalidUserContextException(string message) : Exception(message)
{ }
