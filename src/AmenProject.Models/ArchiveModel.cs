using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class ArchiveModel
    {
        public Guid Id { get; set; }
        public String PathArchive { get; set; }
        public String NomDOc { get; set; }
        public String Commentaire { get; set; }
        public int Etat { get; set; }
        public int ValideArchive { get; set; }
        public DateTimeOffset DateDebutSaisie { get; set; }
        public DateTimeOffset DateFinTypage { get; set; }
       
        public Guid? AgenceId { get; set; }
        public Guid? OperateurId { get; set; }
        public Guid? DocBruteId { get; set; }
        public Guid? OperationId { get; set; }
        public Guid? LotArchiveId { get; set; }
        public DateTimeOffset Datecreation { get; set; }

    }
}
