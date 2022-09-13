using DEMAT.ApplicationServices.Identity;
using DEMAT.Domain.Interfaces;
using DEMAT.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.UpdatePssword
{
    public class UpdatePwdCommandeHandler : IRequestHandler<UpdatePasswordCommande, string>
    {

        private readonly IAuthUserService _AuthUserService;
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        public UpdatePwdCommandeHandler(IAuthUserService authUserService, IAmenUnitOfWork amenUnitOfWork)
        {
            _AuthUserService = authUserService;
            _amenUnitOfWork = amenUnitOfWork;
        }


        public async Task<string> Handle(UpdatePasswordCommande request, CancellationToken cancellationToken)
        {

            var result = await _AuthUserService.UpdatePassword(request.id, request.currentPWD, request.newPWD, request.confirmdPWD, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);
            return result;
        }


    }
}

