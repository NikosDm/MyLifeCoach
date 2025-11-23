using System;

using Libraries.Common.Abstractions.Commands;

using Profile.Api.Core.Dtos.FinancialProfiles.Requests;
using Profile.Api.Core.Dtos.FinancialProfiles.Responses;

namespace Profile.Api.Core.Features.FinancialProfiles.Requests.Commands;

public sealed record UpdateFinancialProfileCommand(Guid Id, UpdateFinancialProfileRequest Request)
    : ICommand<FinancialProfileResponse>;
