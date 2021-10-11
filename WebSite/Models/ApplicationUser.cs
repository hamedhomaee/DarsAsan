using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DarsAsan.Models
{
    public abstract class ApplicationUser : IdentityUser<Guid>
    {
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}