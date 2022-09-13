using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class AllPonderationModel
    {
        public Guid Id { get; set; }
        public string Ponderation { get; set; }
        public int TotalPoints { get; set; }
        public Guid? ControleId { get; set; }
    }
}
