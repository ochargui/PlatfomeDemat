using DEMAT.Core;

using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class AllPonderation :Entity,IAuditable
    {
        public Guid Id { get; set; }
        public string Ponderation { get; set; }
        public int TotalPoints { get; set; }


        public Guid?  ControleId { get; set; }

        #region Relations
        public virtual Controle Controle { get; set; }
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
