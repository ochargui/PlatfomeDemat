using DEMAT.ApplicationServices.Identity;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<AppUser>>
    {
        private readonly IAuthUserService _AuthUserService;

        public GetAllUsersQueryHandler(IAuthUserService authUserService)
        {

            _AuthUserService = authUserService;
        }

       // public async Task<List<(string id, string DisplayName, string userName, string email)>> GetAllUsersAsync()
        public async Task<List<AppUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _AuthUserService.GetAllUsersAsync();
            return users.Select(x => new AppUser()
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToList();
        }
    }
}
