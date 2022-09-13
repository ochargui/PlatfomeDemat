using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class LotArchive : Entity, IAuditable
    {
        public Guid  Id { get; set; }
        public int  CodeLotArchive { get; set; }
        public string  LotArchiveName { get; set; }
        public DateTimeOffset DateFinSaisie { get; set; }


        public virtual ICollection<Archive> Archives { get; set; }



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
