using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateOperator
{
    public class CreateOperateurCommand : IRequest<Guid>
    {
        public String NomOperator { get; set; }
        public String PrenomOperator { get; set; }
        public String loginOperator { get; set; }
        public String passwordOperator { get; set; }
        public DateTimeOffset DateRecrutementOperator { get; set; }
        public String mailOperator { get; set; }
        public String PhoneNumber { get; set; }
        public String RoleOperator { get; set; }
        public String EquipeOperator { get; set; }
        public Guid AgenceID { get; set; }

        internal Operateur ToEntity()
        {
            return new Operateur
            {
                nom = NomOperator,
                prenom = PrenomOperator,
                login = loginOperator,
                password = passwordOperator,
                DateRecrutement =DateRecrutementOperator,
                mail = mailOperator,
                NumTel = PhoneNumber ,
                Role = RoleOperator,
                Equipe= EquipeOperator,
                AgenceId = AgenceID
            };
        }
    }
}
