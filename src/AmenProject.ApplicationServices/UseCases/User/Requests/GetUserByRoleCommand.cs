using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Requests
{
   public class GetUserByRoleCommand:IRequest<IList<string>>
    {
        public string Email { get; set; }

        public GetUserByRoleCommand(string email)
        {
            Email = email;
        }
    }
}
