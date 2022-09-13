using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.EditUserProfile
{
    public class EditUserProfileCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
    
    
}
