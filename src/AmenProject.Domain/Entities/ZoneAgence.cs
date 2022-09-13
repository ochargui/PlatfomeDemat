using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities

{
    public class ZoneAgence : Entity,IAuditable
    {
        public Guid id { get; set; }
        public int codeZoneAgence { get; set; }
        public string ZoneAgenceAdresse { get; set; }

        #region Relation

        public virtual ICollection<Agence> Agences { get; set; }

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
