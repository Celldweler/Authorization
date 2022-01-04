using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Basics.Entities
{    
    /// <summary>
    /// recorde
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public Guid Id { get; set; }

        public string Password { get; set; }
        public string  UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
