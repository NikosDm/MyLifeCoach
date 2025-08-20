using System;
using System.Collections.Generic;

namespace Libraries.Common.Abstractions;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    Guid? UserId { get; }  
    string Username { get; } 
    public string Role { get; }
}
