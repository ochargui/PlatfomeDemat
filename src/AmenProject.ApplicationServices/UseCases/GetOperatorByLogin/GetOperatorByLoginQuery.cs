using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetOperatorByLogin
{
    public class GetOperatorByLoginQuery : IRequest<OperateurModel>
    {
        public string OperatorLogin { get; set; }

        public GetOperatorByLoginQuery(string operatorLogin)
        {
            OperatorLogin = operatorLogin;
        }
    }
}
