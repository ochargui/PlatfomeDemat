using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public  class RapportQuotidienModel
    {
        public string ZoneAgenceName { get; set; }
        public string AgenceCode { get; set; }
        public string AgenceName { get; set; }
        public string OperationName { get; set; }
        public string ControleName { get; set; }
        public int Controle { get; set; }

        public RapportQuotidienModel(string zoneAgenceName, string agenceCode, string agenceName, string operationName, string controleName, int controle)
        {
            ZoneAgenceName = zoneAgenceName;
            AgenceCode = agenceCode;
            AgenceName = agenceName;
            OperationName = operationName;
            ControleName = controleName;
            Controle = controle;
        }
    }
}
