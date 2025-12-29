namespace IdentityServer.Core.Dtos.Requests;

public sealed record CreateUserRequest(string Email, string Password, string Username, string FullName);