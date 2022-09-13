using DEMAT.ApplicationServices.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IAuthUserService _AuthUserService;

        public DeleteUserCommandHandler(IAuthUserService AuthUserService)
        {
           _AuthUserService = AuthUserService;
        }
        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthUserService.DeleteUserAsync(request.Id);

            return result ? 1 : 0;
        }
    }
}


