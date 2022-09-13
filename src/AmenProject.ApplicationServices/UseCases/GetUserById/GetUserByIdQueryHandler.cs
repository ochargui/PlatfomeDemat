using DEMAT.ApplicationServices.Identity;
using DEMAT.ApplicationServices.UseCases.GetUserById;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery,string>
    {
  
        private readonly IAuthUserService _AuthUserService;

        public GetUserByIdQueryHandler( IAuthUserService authUserService)
        {
          
            _AuthUserService = authUserService;
        }

        public async  Task<string> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _AuthUserService.GetUserIdAsync(request.userName, cancellationToken);
        }
    }
}
