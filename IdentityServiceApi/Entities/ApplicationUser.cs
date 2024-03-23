using Microsoft.AspNetCore.Identity;

namespace IdentityServiceApi.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = default!;
}