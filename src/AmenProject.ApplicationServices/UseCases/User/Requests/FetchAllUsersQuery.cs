using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.User.Requests
{
    public class FetchAllUsersQuery :IRequest<IList<string>>
    {
    }
}
