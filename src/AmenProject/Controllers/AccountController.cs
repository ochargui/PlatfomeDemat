using DEMAT.ApplicationServices.Identity;
using DEMAT.ApplicationServices.UseCases.GetUserByEmail;
using DEMAT.ApplicationServices.UseCases.UpdatePssword;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEMAT.ApplicationServices.UseCases.GetUserById;
using DEMAT.ApplicationServices.UseCases.GetAllUsers;
using DEMAT.ApplicationServices.UseCases.CreateUser;
using DEMAT.ApplicationServices.UseCases.DeleteUser;
using DEMAT.ApplicationServices.UseCases.EditUserProfile;
using Microsoft.AspNetCore.Authorization;
using DEMAT.Models.Dtos;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthUserService _authService;

        public AccountController(IMediator mediator, 
            ILogger<AccountController> logger,
            IAuthUserService authService
           )
        {
            _mediator = mediator;
            _logger = logger;
            _authService = authService;
           
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var role = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            var user = await _authService.GetCurrentUser(email, role);

            return Ok(user);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AppUser>> GetUserByEmail()
        {
            var result = await _mediator.Send(new GetUserByEmailQuery());
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponse>> Login ([FromBody] AuthRequest request)
        {
            var user = await _authService.Login(request);
            return Ok(user);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> SendResetPasswordEmail(string email)
        {
            var response = await _authService.SendResetEmail(email);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ResetPassword(string email, string newPassword, string identityToken, string secondSecurityLayerToken)
        {
            var response = await _authService.ResetPassword(email,newPassword,identityToken, secondSecurityLayerToken);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdatePassword([FromQuery] string id, string currentPWD, string newPWD, string confirmdPWD)
        {
            var result = await _mediator.Send(new UpdatePasswordCommande(id, currentPWD, newPWD, confirmdPWD));
            return Ok(result);



        }


        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AppUser>> GetUserIdAsync([FromQuery] string userName)
        {
            //Parameters Validation.			
            if (userName == null || userName== string.Empty)
            {
                return BadRequest("uer Id is mandatory !");
            }
            var result = await _mediator.Send(new GetUserByIdQuery(userName));
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<AppUser>))]
        public async Task<IActionResult> GetAllUserAsync()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("Delete/{userId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = userId });
            return Ok(result);
        }

        [HttpPut("EditUserProfile/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditUserProfile(string id, [FromBody] EditUserProfileCommand command)
        {
            if (id == command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
