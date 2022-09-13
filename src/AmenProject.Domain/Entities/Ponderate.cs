using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities

{
    public  class Ponderate : Entity, IAuditable
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Valeur { get; set; }

        public Guid? OperationId { get; set; }

        #region Relations

        public virtual Operation Operation { get; set; }
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
