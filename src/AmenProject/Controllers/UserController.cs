using DEMAT.ApplicationServices.UseCases.User.Requests;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator,
          ILogger<UserController> logger
         )
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityResult>> RegisterUser(string firstName , string lastName , string email , string phoneNumber , string password, List<string> roles)
        {
            var result = await _mediator.Send(new RegisterUserCommand(firstName , lastName , email , phoneNumber , password, roles));
            return result;
        }
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<string>>FetchAllUsers()
        {
            var result = await _mediator.Send(new FetchAllUsersQuery());
            return result;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityResult>> AffectRoleToUser(string role,string email)
        {
            var result = await _mediator.Send(new AffectUserCommand(role,email));
            return result;
        }


        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityResult>> UnassignRoleToUser(string role, string email)
        {
            var result = await _mediator.Send(new UnassignRoleCommand(role, email));
            return result;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<string>> GetUserByRole(string email)
        {
            var result = await _mediator.Send(new GetUserByRoleCommand(email));
            return result;
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityResult>> UpdateUser(string firstName, string lastName, string email, string phoneNumber , string index)
        {
            var result = await _mediator.Send(new UpdateUserCommand(firstName , lastName , email , phoneNumber , index));
            return result;
        }

        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityResult>> DeleteUser(string email)
        {
            var result = await _mediator.Send(new RemoveUserCommand(email));
            return result;
        }
    }
}
