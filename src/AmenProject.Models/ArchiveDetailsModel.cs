using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class ArchiveDetailsModel
    {
        private ArchiveDetailsModel archive;
        

        public ArchiveDetailsModel()
        {
        }

        public ArchiveDetailsModel(ArchiveDetailsModel archive)
        {
            this.archive = archive;
        }

      

        public Guid Id { get; set; }
        public String PathArchive { get; set; }
        public String NomDOc { get; set; }
        public String Commentaire { get; set; }
        public int Etat { get; set; }
        public int ValideArchive { get; set; }
        public Guid? AgenceId { get; set; }
        public Guid? OperateurId { get; set; }
        public Guid? DocBruteId { get; set; }
        public Guid? OperationId { get; set; }
        public Guid? LotArchiveId { get; set; }
        public DateTimeOffset DateDebutSaisie { get; set; }
        public DateTimeOffset DateFinTypage { get; set; }


        public ArchiveDetailsModel(ArchiveDetailsModel archive, Guid id, string pathArchive, string nomDOc, string commentaire, int etat, int valideArchive, Guid? agenceId, Guid? operateurId, Guid? docBruteId, Guid? operationId, DateTimeOffset datecreation, DateTimeOffset dateUpdate) : this(archive)
        {
            Id = id;
            PathArchive = pathArchive;
            NomDOc = nomDOc;
            Commentaire = commentaire;
            Etat = etat;
            ValideArchive = valideArchive;
            AgenceId = agenceId;
            OperateurId = operateurId;
            DocBruteId = docBruteId;
            OperationId = operationId;
            DateDebutSaisie = datecreation;
            DateFinTypage = dateUpdate;
        }

        public ArchiveDetailsModel(string pathArchive, string nomDOc, string commentaire, int etat, int valideArchive, Guid? agenceId, Guid? operateurId, Guid? docBruteId, Guid? operationId, DateTimeOffset datecreation)
        {
            PathArchive = pathArchive;
            NomDOc = nomDOc;
            Commentaire = commentaire;
            Etat = etat;
            ValideArchive = valideArchive;
            AgenceId = agenceId;
            OperateurId = operateurId;
            DocBruteId = docBruteId;
            OperationId = operationId;
            DateDebutSaisie = datecreation;
        }
    }
}
