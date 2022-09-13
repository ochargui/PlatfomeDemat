using DEMAT.ApplicationServices.UseCases.CreateInputOputDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMAT.Api.Requests
{
    public class CreateInputOutputDirectoryRequest
    { internal CreatenputOutputDirectoryCommand ToCommand()
        {
            return new CreatenputOutputDirectoryCommand {};
        }



    }


}
