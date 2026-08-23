using System;

namespace Profiles.Api.Core.Dtos.Users.Requests;

public sealed record ChangeUserStatusRequest(bool SetActive);