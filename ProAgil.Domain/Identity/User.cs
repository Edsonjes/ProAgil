using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProAgil.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]

        public string fullName { get; set; }
        
        public List<UserRole> UserRoles { get; set; }
    }
}