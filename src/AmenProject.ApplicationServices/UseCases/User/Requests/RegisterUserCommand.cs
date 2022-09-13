using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Requests
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public RegisterUserCommand(string firstName, string lastName, string email , string phoneNumber , string password, List<string> roles)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Roles = roles;
        }
    }
}
