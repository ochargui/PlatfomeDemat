using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Requests
{
    public class RemoveUserCommand : IRequest<IdentityResult>
    {
        public string Email { get; set; }

        public RemoveUserCommand( string email)
        {
            Email = email;
        }
    }
}
