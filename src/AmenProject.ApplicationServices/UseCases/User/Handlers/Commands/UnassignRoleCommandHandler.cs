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
    public class UnassignRoleCommandHandler : IRequestHandler<UnassignRoleCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;



        public UnassignRoleCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> Handle(UnassignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var result = await _userManager.RemoveFromRoleAsync(user, request.Role);

            return result;
        }
    }
}
