using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class Operation  : Entity,IAuditable
    {
        public Guid Id { get; set; }
        public int CodeOperation { get; set; }
        public string OperationName  { get; set; }
        public int Ponderation { get; set; }


        #region Relatons 

        public virtual ICollection<Ponderate> Ponderates { get; set; }
        public virtual ICollection<Archive> Archives { get; set; }
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
