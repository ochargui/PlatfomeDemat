using DEMAT.ApplicationServices.Sendgrid;
using DEMAT.ApplicationServices.UseCases.CreateUser;
using DEMAT.ApplicationServices.UseCases.User.Requests;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models.Sendgrid;
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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
    
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;




        public RegisterUserCommandHandler( UserManager<AppUser> userManager, IEmailSender emailSender)
        { 
            _userManager = userManager;
            _emailSender = emailSender;

        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser()
            {
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("There is an error in the add user , re-check the code");
            }

            var addUserRole = await _userManager.AddToRolesAsync(user, request.Roles);
            if (!addUserRole.Succeeded)
            {
                throw new Exception("There is an error in the add user role , re-check the code");
            }

            var emailToSend = new Email
            {
                To = request.Email,
                Body = "Votre compte a été crée avec succés , voici votre mot de passe " + request.Password,
                Subject = "Votre mot de passe de votre nouveau compte"
            };

            await _emailSender.SendEmail(emailToSend);

            return result;
           
        }
    }
}
