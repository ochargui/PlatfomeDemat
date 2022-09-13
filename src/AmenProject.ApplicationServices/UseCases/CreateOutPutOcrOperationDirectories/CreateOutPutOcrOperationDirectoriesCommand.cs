using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateOutPutOcrOperationDirectories
{
    public class CreateOutPutOcrOperationDirectoriesCommand : IRequest<string>
    {
        public string journee { get; set; }
        public int AgenceCode { get; set; }

        public CreateOutPutOcrOperationDirectoriesCommand(string journee, int agenceCode)
        {
            this.journee = journee;
            AgenceCode = agenceCode;
        }
    }
}
