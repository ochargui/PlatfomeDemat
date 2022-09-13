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

namespace DEMAT.ApplicationServices.UseCases.User.Handlers.Queries
{
    public class GetUserByRoleCommandHandler : IRequestHandler<GetUserByRoleCommand, IList<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        public GetUserByRoleCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IList<string>> Handle(GetUserByRoleCommand request, CancellationToken cancellationToken)
        {
            var user= await  _userManager.FindByEmailAsync(request.Email);
            var role = await _userManager.GetRolesAsync(user);
            return role;
        }
    }
}
