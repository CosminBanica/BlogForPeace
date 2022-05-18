using BlogForPeace.Api.Authorization;
using BlogForPeace.Api.Features.Profiles.RegisterProfile;
using BlogForPeace.Api.Features.Profiles.ViewProfile;
using BlogForPeace.Api.Features.Profiles.EditProfile;
using Microsoft.AspNetCore.Authorization;
using BlogForPeace.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using BlogForPeace.Core.DataModel;
using Microsoft.Extensions.Options;
using System.Net;

namespace BlogForPeace.Api.Features.Profiles
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IRegisterProfileCommandHandler registerProfileCommandHandler;
        private readonly IViewProfileQueryHandler viewProfileQueryHandler;
        private readonly IEditProfileCommandHandler editProfileCommandHandler;
        private readonly BlogForPeaceContext dbContext;

        public ProfilesController(
            IRegisterProfileCommandHandler _registerProfileCommandHandler, 
            IViewProfileQueryHandler _viewProfileQueryHandler,
            IEditProfileCommandHandler _editProfileCommandHandler,
            BlogForPeaceContext _dbContext)
        {
            registerProfileCommandHandler = _registerProfileCommandHandler;
            viewProfileQueryHandler = _viewProfileQueryHandler;
            editProfileCommandHandler = _editProfileCommandHandler;
            dbContext = _dbContext;
        }

        [HttpPost("registerProfile")]
        [Authorize]
        public async Task<IActionResult> RegisterProfile(CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            var user = dbContext.Users
                .Where(x => x.IdentityId == identityId)
                .Select(x => new ProfileDto(x.Email, x.Name, x.Address, new List<Tags>()))
                .FirstOrDefault();

            if (user == null)
            {
                await registerProfileCommandHandler.HandleAsync(identityId, cancellationToken);
            }

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet("viewProfile")]
        [Authorize]
        public async Task<ActionResult<ProfileDto>> ViewProfileAsync(CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            var profile = await viewProfileQueryHandler.HandleAsync(identityId, cancellationToken);

            return Ok(profile);
        }

        [HttpPut("editProfile")]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromBody] EditProfileCommand command, CancellationToken cancellationToken)
        {
            await editProfileCommandHandler.HandleAsync(command, cancellationToken);

            return Ok();
        }
    }
}
