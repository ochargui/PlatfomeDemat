using DEMAT.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace FrameWork.Core
{
    /// <summary>
    /// Base classe for auditable entity
    /// </summary>
    /// <seealso cref="FrameWork.Core.Entity" />
    /// <seealso cref="FrameWork.Core.IAuditable" />
    public class AuditableEntity : Entity, IAuditable
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created by identifier.
        /// </summary>
        /// <value>
        /// The created by identifier.
        /// </value>
        public string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTimeOffset? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by identifier.
        /// </summary>
        /// <value>
        /// The modified by identifier.
        /// </value>
        public string ModifiedById { get; set; }
    }
}
