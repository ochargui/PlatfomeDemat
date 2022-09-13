using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Requests
{
    public class UpdateUserCommand : IRequest<IdentityResult>
    {
        public string Index { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public UpdateUserCommand(string firstName, string lastName, string email, string phoneNumber , string index)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Index = index;
        }
    }
}
