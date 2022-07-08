using washapp.services.identity.application.Commands;
using washapp.services.identity.application.Commands.Handlers;
using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Queries;
using washapp.services.identity.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace washapp.services.identity.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        [Route("signUp")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _mediator.Send(new RegisterUser(dto));
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signIn")]
        public async Task<ActionResult<AuthDto>> Login([FromBody] SignIn signInRequest)
        {
            var authDto = await _mediator.Send(signInRequest);
            return Ok(authDto);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshAccessToken refreshAccessToken)
        {
            var authDto = await _mediator.Send(refreshAccessToken);
            return Ok(authDto);
        }

        [HttpPost]
        [Authorize]
        [Route("changePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePassword command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "Administrator")]
        [Route("remove")]
        public async Task<ActionResult> RemoveUser([FromHeader]Guid userId)
        {
            DeleteUser command = new DeleteUser() {UserId = userId};
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("getAuthUserDetails")]
        public async Task<ActionResult<GetUserDto>> GetAuthorizedUser()
        {
            var query = new GetAuthorizedUser();
            var userDto = await _mediator.Send(query);
            return Ok(userDto);
        }
        [HttpGet]
        [Authorize(Policy = "Administrator")]
        [Route("all")]
        public async Task<ActionResult<GetUserDto>> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsers());
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("refreshToken/revoke")]
        public async Task<ActionResult> RevokeToken([FromHeader] Guid userId)
        {
            await _mediator.Send(new RevokeUserToken() {UserId = userId});
            return Ok();
        }
        
        [HttpPost]
        [Authorize]
        [Route("refreshToken/revokeAll")]
        public async Task<ActionResult> RevokeAllTokens()
        {
            await _mediator.Send(new RevokeAllRefreshTokens());
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUser request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [Authorize(Policy = "Administrator")]
        [HttpPut]
        [Route("changeRole")]
        public async Task<ActionResult> ChangeRole([FromBody] ChangeRole request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
