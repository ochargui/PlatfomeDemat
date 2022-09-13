using DEMAT.ApplicationServices.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IAuthUserService _AuthUserService;
        public CreateUserCommandHandler(IAuthUserService authUserService)
        {
            _AuthUserService = authUserService;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthUserService.CreateUserAsync(request.DisplayName,request.UserName, request.Password, request.Email,request.FirstName,request.LastName, request.Roles);
            return result.isSucceed ? 1 : 0;
        }
    }
}