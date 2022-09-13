
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class Agence : Entity,IAuditable
    {
        public Guid Id { get; set; }
        public int CodeAgence { get; set; }
        public string NomAgence { get; set; }
        public string Adresse { get; set; }
        public Guid? ZoneAgenceId { get; set; }


        #region Relation

        public virtual ZoneAgence ZoneAgence { get; set; }
        public virtual ICollection<Archive> Archives { get; set; }
        public virtual ICollection<Operateur> Operateurs { get; set; }

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
