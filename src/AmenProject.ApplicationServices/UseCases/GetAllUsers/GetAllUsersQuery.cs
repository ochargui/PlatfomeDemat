using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<AppUser>>
    {
   
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
