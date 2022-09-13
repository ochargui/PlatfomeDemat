using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class LotPacket : Entity, IAuditable
    {

        public Guid Id { get; set; }
        public string NomPAcket { get; set; }
        public int EtatLotPacket { get; set; }
        public int NombreDoc { get; set; }


        #region Relations 

        public virtual ICollection<DocBrute> DocBrutes { get; set; }
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
