using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class ControleModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int CodeAnomalie { get; set; }
        public int CodeControle { get; set; }
        public Guid? GroupeControleId { get; set; }
    }
}
