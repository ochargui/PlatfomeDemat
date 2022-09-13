using DEMAT.ApplicationServices.UseCases.User.Requests;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
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
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, IdentityResult>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IDematContext _amenBankContext;
        private readonly UserManager<AppUser> _userManager;



        public RemoveUserCommandHandler(IAmenUnitOfWork amenUnitOfWork, IDematContext amenBankContext, UserManager<AppUser> userManager)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _amenBankContext = amenBankContext;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new Exception("There is no user with such an email");

            var result = await _userManager.DeleteAsync(user);

            return result;
        }
    }
}
