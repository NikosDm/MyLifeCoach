namespace IdentityServer.Core.Dtos.Requests;

public sealed record LoginUserRequest(string Username, string Password, bool RememberLogin);