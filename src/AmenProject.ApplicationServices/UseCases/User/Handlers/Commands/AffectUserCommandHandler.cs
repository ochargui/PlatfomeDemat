using DEMAT.ApplicationServices.UseCases.User.Requests;
using DEMAT.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Handlers.Commands
{
    public class AffectUserCommandHandler : IRequestHandler<AffectUserCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;
        


        public AffectUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(AffectUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var result = await _userManager.AddToRoleAsync(user, request.Role);

            return result;
        }
    }
}
