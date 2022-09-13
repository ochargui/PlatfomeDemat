using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Domain.Entities
{
    public class DocumentPicture
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}
