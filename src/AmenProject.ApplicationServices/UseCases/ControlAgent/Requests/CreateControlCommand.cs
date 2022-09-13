using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Requests
{
    public class CreateControlCommand : IRequest<Control>
    {

        public int FieldNumberScore { get; set;}
        public int ClientSignatureScore { get; set; }
        public int BankStampScore { get; set; }
        public int ScoreLimit { get; set; }
        public string Name { get; set; }
        public CreateControlCommand(int fieldNumberScore, int clientSignatureScore, int bankStampScore, int scoreLimit, string name)
        {
            FieldNumberScore = fieldNumberScore;
            ClientSignatureScore = clientSignatureScore;
            BankStampScore = bankStampScore;
            ScoreLimit = scoreLimit;
            Name = name;

        }

    }
}
