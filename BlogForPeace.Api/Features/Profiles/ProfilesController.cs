using BlogForPeace.Api.Authorization;
using BlogForPeace.Api.Features.Profiles.RegisterProfile;
using BlogForPeace.Api.Features.Profiles.ViewProfile;
using Microsoft.AspNetCore.Authorization;
using BlogForPeace.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using BlogForPeace.Core.DataModel;
using Microsoft.Extensions.Options;

namespace BlogForPeace.Api.Features.Profiles
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IRegisterProfileCommandHandler registerProfileCommandHandler;
        private readonly IViewProfileQueryHandler viewProfileQueryHandler;
        private readonly BlogForPeaceContext dbContext;
        private readonly JWTConfig jwtConfig;

        public ProfilesController(
            IRegisterProfileCommandHandler _registerProfileCommandHandler, 
            IViewProfileQueryHandler _viewProfileQueryHandler,
            BlogForPeaceContext _dbContext,
            IOptions<JWTConfig> _jwtConfig)
        {
            registerProfileCommandHandler = _registerProfileCommandHandler;
            viewProfileQueryHandler = _viewProfileQueryHandler;
            dbContext = _dbContext;
            jwtConfig = _jwtConfig.Value;
        }

        private string GenerateToken(ProfileDto user)
        {
            var claims = new List<System.Security.Claims.Claim>(){
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Email),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtConfig.Audience,
                Issuer = jwtConfig.Issuer
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private async Task RegisterProfileAsync(RegisterProfileCommand command, CancellationToken cancellationToken)
        {
            await registerProfileCommandHandler.HandleAsync(command, command.Email, cancellationToken);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginInfo, CancellationToken cancellationToken)
        {
            var user = dbContext.Users
                .Where(x => x.Email == loginInfo.Email)
                .Select(x => new ProfileDto(x.Email, x.Name, x.Address, new List<Tags>()))
                .FirstOrDefault();

            if (user == null)
            {
                await RegisterProfileAsync(new RegisterProfileCommand(loginInfo.Email, "", ""), cancellationToken);

                user = dbContext.Users
                    .Where(x => x.Email == loginInfo.Email)
                    .Select(x => new ProfileDto(x.Email, x.Name, x.Address, new List<Tags>()))
                    .FirstOrDefault();

                if (user == null)
                {
                    return Unauthorized();
                }
            }

            var token = GenerateToken(user);

            return Ok(token);
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
    }
}
