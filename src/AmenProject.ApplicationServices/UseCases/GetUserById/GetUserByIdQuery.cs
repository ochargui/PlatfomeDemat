using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetUserById
{
    public class GetUserByIdQuery : IRequest<string>
    {
        public  string userName { get; set; }

        public GetUserByIdQuery(string userId)
        {
            userName = userId;
        }
    }
}
