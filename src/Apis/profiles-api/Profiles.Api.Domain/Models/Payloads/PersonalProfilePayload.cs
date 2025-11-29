using System;

namespace Profiles.Api.Domain.Models.Payloads;

public class PersonalProfilePayload
{
    public string FullName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
