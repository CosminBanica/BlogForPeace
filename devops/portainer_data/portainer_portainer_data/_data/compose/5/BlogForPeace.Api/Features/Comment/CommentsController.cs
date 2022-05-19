using BlogForPeace.Api.Features.Comment.AddComment;
using BlogForPeace.Api.Features.Comment.DislikeComment;
using BlogForPeace.Api.Features.Comment.LikeComment;
using BlogForPeace.Api.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BlogForPeace.Api.Features.Comment
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IAddCommentCommandHandler addCommentCommandHandler;
        private readonly IDislikeCommentCommandHandler dislikeCommentCommandHandler;
        private readonly ILikeCommentCommandHandler likeCommentCommandHandler;

        public CommentsController(
            IAddCommentCommandHandler _addCommentCommandHandler,
            IDislikeCommentCommandHandler _dislikeCommentCommandHandler,
            ILikeCommentCommandHandler _likeCommentCommandHandler)
        {
            this.addCommentCommandHandler = _addCommentCommandHandler;
            this.dislikeCommentCommandHandler = _dislikeCommentCommandHandler;
            this.likeCommentCommandHandler = _likeCommentCommandHandler;
        }

        [HttpPut("addComment")]
        [Authorize]
        public async Task<IActionResult> AddCommentAsync([FromBody] AddCommentCommand command, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await addCommentCommandHandler.HandleAsync(command, identityId, cancellationToken);

            return NoContent();
        }

        [HttpPut("likeComment/{id}")]
        [Authorize]
        public async Task<IActionResult> LikeCommentAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await likeCommentCommandHandler.HandleAsync(id, identityId, cancellationToken);

            return NoContent();
        }

        [HttpPut("dislikeComment/{id}")]
        [Authorize]
        public async Task<IActionResult> DislikeCommentAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await dislikeCommentCommandHandler.HandleAsync(id, identityId, cancellationToken);

            return NoContent();
        }
    }
}
