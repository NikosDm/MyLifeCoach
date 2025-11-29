using System;

using Libraries.Common.Abstractions.Commands;

using Profiles.Api.Core.Dtos.FinancialProfiles.Requests;
using Profiles.Api.Core.Dtos.FinancialProfiles.Responses;

namespace Profiles.Api.Core.Features.FinancialProfiles.Requests.Commands;

public sealed record UpdateFinancialProfileCommand(Guid Id, UpdateFinancialProfileRequest Request)
    : ICommand<FinancialProfileResponse>;
