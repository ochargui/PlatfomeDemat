using DEMAT.Core;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class DocBrute : Entity, IAuditable
    {
        public Guid Id { get; set; }     
        public string NomDoc { get; set; }
        public string Commentaire { get; set; }
        public int Etat { get; set; }
       

        #region Relations
       
        public Guid? LotPacketId { get; set; }
        public Guid? AgenceId { get; set; }
      
        public virtual Agence Agence { get; set; }


        public virtual Archive Archive { get; set; }
        public virtual LotPacket LotPacket { get; set; }
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
