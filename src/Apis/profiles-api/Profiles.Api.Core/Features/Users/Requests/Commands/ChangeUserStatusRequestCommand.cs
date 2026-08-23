using System;

using Libraries.Common.Abstractions.Commands;

using Profiles.Api.Core.Dtos.Users.Requests;
using Profiles.Api.Core.Dtos.Users.Responses;

namespace Profiles.Api.Core.Features.Users.Requests.Commands;

public sealed record ChangeUserStatusRequestCommand(Guid UserId, ChangeUserStatusRequest Request) : ICommand<UserResponse>;