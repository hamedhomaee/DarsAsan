using System;
using Microsoft.AspNetCore.Identity;

namespace DarsAsan.Models
{
    public abstract class ApplicationUser : IdentityUser<Guid>
    {
    }
}