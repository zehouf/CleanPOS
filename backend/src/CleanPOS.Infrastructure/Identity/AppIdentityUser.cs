// src/CleanPOS.Infrastructure/Identity/AppIdentityUser.cs
namespace CleanPOS.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

public class AppIdentityUser : IdentityUser
{
    public string? FullName { get; set; }
}