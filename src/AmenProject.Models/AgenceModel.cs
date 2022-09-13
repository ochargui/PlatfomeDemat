using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class AgenceModel
    {
        public Guid Id { get; set; }
        public int CodeAgence { get; set; }
        public string NomAgence { get; set; }
        public string Adresse { get; set; }
        public Guid? ZoneAgenceId { get; set; }
    }
}
