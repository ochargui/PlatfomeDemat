
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class Data : Entity, IAuditable
    {
        public Guid Id { get; set; }
        public string ControleValue { get; set; }

        public Guid? ArchiveId { get; set; }
        public Guid? ControleId { get; set; }

        #region Relations
        public virtual Controle Controle { get; set; }
        public virtual Archive Archive { get; set; }
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
