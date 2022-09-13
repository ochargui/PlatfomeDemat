using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class DocumentsEnAttenteModel
    {
        public Guid idAgence { get; set; }
        public int  CodeAgence { get; set; }
        public string NomAgence  { get; set; }
        public int DocEnAttente { get; set; }
    }
}
