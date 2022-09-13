using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DEMAT.Infrastructure.Identity.Models
{
    public class AppUser : IdentityUser
    {

        public string DisplayName { get; set; }
        
        public string ResetToken { get; set; }

        public string ExpirationDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
    }
}
