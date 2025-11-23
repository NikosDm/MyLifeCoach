using System;

namespace Profile.Api.Core.Dtos.PersonalProfiles.Requests;

public sealed record UpdatePersonalProfileRequest(
    string FullName,
    DateTimeOffset DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber)
    : BasePersonalProfileRequest(FullName, DateOfBirth, City, Country, Email, PhoneNumber);