using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class Controle : Entity, IAuditable
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CodeAnomalie { get; set; }
        public int CodeControle{ get; set; }

        public Guid? GroupeControleId { get; set; }


        #region Relations
        public virtual GroupeControle GroupeControle { get; set; }
        public virtual ICollection<Data> DataList { get; set; }
        public virtual ICollection<AllPonderation> AllPonderations { get; set; }
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
