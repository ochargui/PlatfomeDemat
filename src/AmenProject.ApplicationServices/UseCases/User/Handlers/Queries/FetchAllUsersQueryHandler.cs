using DEMAT.ApplicationServices.UseCases.User.Requests;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Handlers.Queries
{
    class FetchAllUsersQueryHandler : IRequestHandler<FetchAllUsersQuery, IList<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        public FetchAllUsersQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<string>> Handle(FetchAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.Select(u =>
             string.Join(",", _userManager.GetRolesAsync(u).Result.ToArray())
            ).ToListAsync();
        }
    }
}
