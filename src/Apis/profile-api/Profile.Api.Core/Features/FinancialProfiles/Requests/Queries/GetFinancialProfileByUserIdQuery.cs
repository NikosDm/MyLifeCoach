using System;

using Libraries.Common.Abstractions.Queries;

using Profile.Api.Core.Dtos.FinancialProfiles.Responses;

namespace Profile.Api.Core.Features.FinancialProfiles.Requests.Queries;

public sealed record GetFinancialProfileByUserIdQuery(Guid UserId) : IQuery<FinancialProfileResponse>;