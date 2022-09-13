using DEMAT.Infrastructure.Identity.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetUserByEmail
{
    public class GetUserByEmailQuery :IRequest<AppUser>
    {
    }
}
