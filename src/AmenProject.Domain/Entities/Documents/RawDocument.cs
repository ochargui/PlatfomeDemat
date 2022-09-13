using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Domain.Entities.Documents
{
    public class RawDocument : Entity, IAuditable
    {
        public Guid Id { get; set; }
        public string DocumentName { get; set; }
        public string Observation { get; set; }
        public DocumentType DocumentType { get; set; }
        public StateTypes State { get; set; }
        public string ControledBy { get; set; }


        #region Relation
        public virtual DocumentPicture DocumentPicture { get; set; }
        public virtual DocumentFields DocumentFields { get; set; }
        public virtual Lot Lot { get; set; }
        public Control Control { get; set; }
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
