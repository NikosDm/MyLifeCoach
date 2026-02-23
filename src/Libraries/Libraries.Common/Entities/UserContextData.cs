using System;

namespace Libraries.Common.Entities;

public sealed record UserContextData(bool IsAuthenticated, Guid? UserId, string Username, string Role);