using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.SinIn
{
    public class SinInCommand : IRequest<OperateurModel>
    {
        public string Login { get; set; }
        public string Pwd { get; set; }

        public SinInCommand(string login, string pwd)
        {
            Login = login;
            Pwd = pwd;
        }
    }
}
