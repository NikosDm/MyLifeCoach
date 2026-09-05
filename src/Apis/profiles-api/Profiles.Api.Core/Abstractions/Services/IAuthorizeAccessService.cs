using System;

namespace Profiles.Api.Core.Abstractions.Services;

public interface IAuthorizeAccessService
{
    bool CanUserAccessProfileAction(Guid userId);
}