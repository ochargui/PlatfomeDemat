using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateGroupeControle
{
    public class CreatGroupeControleCommand :  IRequest<Guid>
    {
        public String Nom { get; set; }
        public String GroupePonderation { get; set; }

        internal GroupeControle ToEntity()
        {
            return new GroupeControle
            {
                Name = Nom,
                GroupePond = GroupePonderation
            };
        }
    }
}
