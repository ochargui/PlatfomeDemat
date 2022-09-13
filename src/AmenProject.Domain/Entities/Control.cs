using DEMAT.Core;
using DEMAT.Domain.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Domain.Entities
{
    public class Control : Entity, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int FieldNumberScore { get; set; }
        public int ClientSignatureScore { get; set; }
        public int BankStampScore { get; set; }
        public int ScoreLimit { get; set; }

        #region Relation
        public virtual ICollection<RawDocument> Documents { get; set; }
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
