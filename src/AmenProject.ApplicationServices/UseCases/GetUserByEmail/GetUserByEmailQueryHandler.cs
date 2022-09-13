using DEMAT.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetUserByEmail
{
    class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, AppUser>
    {
        private readonly UserManager<AppUser> _userManager;
        public GetUserByEmailQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public Task<AppUser> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.FindByEmailAsync("admin@admin.com");
            return user;
        }
    }
}
