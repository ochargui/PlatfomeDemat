using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Requests
{
    public class AffectUserCommand : IRequest<IdentityResult>
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public AffectUserCommand(string role,string email)
        {
            this.Role = role;
            this.Email = email;

        }
    }
}
