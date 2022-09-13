using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateArchive
{
    public class CreateArchiveCommand : IRequest<Guid>
    {
        public String Path { get; set; }
        public String ArchiveNomDOc { get; set; }
        public String ArchiveCommentaire { get; set; }
        public int EtatArchive { get; set; }
        public int Valide { get; set; }
        public Guid? IdAgence { get; set; }
        public Guid? IdOperateur { get; set; }
        public Guid? IdDocBrute{ get; set; }
        public Guid? IdOperation { get; set; }
        public Guid? IdLot{ get; set; }
        public DateTimeOffset dateDebutSaisie{ get; set; }
        public DateTimeOffset dateFinSaisie{ get; set; }

        internal Archive ToEntity()
        {
            return new Archive
            {
                PathArchive = Path,
                NomDOc = ArchiveNomDOc,
                Commentaire = ArchiveCommentaire,
                Etat =EtatArchive,
                ValideArchive = Valide,
                AgenceId = IdAgence,
                OperateurId = IdOperateur,
                DocBruteId = IdDocBrute,
                OperationId = IdOperation,
                LotArchiveId = IdLot,
                CreatedDate =  DateTimeOffset.Now,
              
            }; 
        }
    }
}
