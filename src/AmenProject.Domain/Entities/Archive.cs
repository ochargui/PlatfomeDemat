
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
   public class Archive : Entity,IAuditable
    {
        public Guid Id { get; set; }
        public string PathArchive { get; set; }
        public string NomDOc { get; set; }
        public string Commentaire { get; set; }
        public int  Etat{ get; set; }
        public int ValideArchive { get; set; }
        public DateTimeOffset DateFinSaisie { get; set; }

        public Guid? AgenceId { get; set; }
        public Guid? OperateurId { get; set; }
        public Guid? DocBruteId { get; set; }
        public Guid? OperationId { get; set; }
        public Guid? LotArchiveId { get; set; }

        #region Realation
        public virtual Agence Agence { get; set; }
        public virtual Operateur Operateur { get; set; } 
        public virtual DocBrute DocBrute { get; set; }
        public virtual Operation Operation { get; set; } // one to one 
        public virtual LotArchive LotArchive { get; set; }
        public virtual ICollection<Data> DataList { get; set; }

        #endregion

        #region Auditable
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedById { get; set; }
        #endregion
    }
}
