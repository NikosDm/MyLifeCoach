using System;

namespace Profiles.Api.Core.Dtos.PersonalProfiles.Requests;

public sealed record UpdatePersonalProfileRequest(
    Guid UserId,
    string FullName,
    DateTimeOffset? DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber)
    : BasePersonalProfileRequest(UserId, FullName, DateOfBirth, City, Country, Email, PhoneNumber);