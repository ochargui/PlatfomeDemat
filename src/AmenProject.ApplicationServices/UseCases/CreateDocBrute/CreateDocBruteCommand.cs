using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateDocBrute
{
    public class CreateDocBruteCommand : IRequest<Guid>
    {
        public String NomDocument { get; set; }
        public String Comment { get; set; }
        public int EtatDocument { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset DebutTypageDate { get; set; }
        public DateTimeOffset FinTypageDate { get; set; }
        public Guid LotPacketID { get; set; }
        public Guid AgenceID { get; set; }


        internal  DocBrute ToEntity()
        {
            return new DocBrute
            {   NomDoc = NomDocument,
                Commentaire = Comment,
                Etat = EtatDocument,
                LotPacketId = LotPacketID,
                AgenceId = AgenceID
            };
        }

    }


}
