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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IdentityResult>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IDematContext _amenBankContext;
        private readonly UserManager<AppUser> _userManager;



        public UpdateUserCommandHandler(IAmenUnitOfWork amenUnitOfWork, IDematContext amenBankContext, UserManager<AppUser> userManager)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _amenBankContext = amenBankContext;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Index);

            if (user == null) throw new Exception("There is no user with such an email");

            user.UserName = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;


            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}
