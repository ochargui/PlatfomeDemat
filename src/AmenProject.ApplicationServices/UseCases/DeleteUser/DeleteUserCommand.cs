using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.DeleteUser
{
    public class DeleteUserCommand : IRequest<int>
    {
        public string Id { get; set; }
    }
}
