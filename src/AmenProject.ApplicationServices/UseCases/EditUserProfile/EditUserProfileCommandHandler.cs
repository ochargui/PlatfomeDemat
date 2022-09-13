using DEMAT.ApplicationServices.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.EditUserProfile
{
    public class EditUserProfileCommandHandler: IRequestHandler<EditUserProfileCommand, int>
    {
        private readonly IAuthUserService _AuthUserService;

        public EditUserProfileCommandHandler(IAuthUserService AuthUserService)
        {
            _AuthUserService = AuthUserService;
        }
        public async Task<int> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthUserService.UpdateUserProfile(request.Id, request.LastName,request.FirstName, request.Email, request.Roles);
            return result ? 1 : 0;
        }
    }
}


