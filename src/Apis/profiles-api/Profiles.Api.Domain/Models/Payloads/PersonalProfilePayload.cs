using System;
using System.Collections.Generic;

namespace Profiles.Api.Domain.Models.Payloads;

public class PersonalProfilePayload
{
    public string Username { get; set; }
    public string FullName { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Bio { get; set; }
    public string ProfilePictureUrl { get; set; }
    public IEnumerable<string> Hobbies { get; set; }
}
