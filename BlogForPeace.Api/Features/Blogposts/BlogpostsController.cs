using BlogForPeace.Api.Features.Blogposts.AddBlogpost;
using BlogForPeace.Api.Features.Blogposts.ViewAllBlogposts;
using BlogForPeace.Api.Features.Blogposts.ViewBlogpost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogForPeace.Api.Features.Blogposts
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogpostsController : ControllerBase
    {
        private readonly IAddBlogpostCommandHandler addBlogpostCommandHandler;
        private readonly IViewAllBlogpostsQueryHandler viewAllBlogpostsQueryHandler;
        private readonly IViewBlogpostQueryHandler viewBlogpostQueryHandler;

        public BlogpostsController(
            IAddBlogpostCommandHandler _addBlogpostCommandHandler,
            IViewAllBlogpostsQueryHandler _viewAllBlogpostsQueryHandler,
            IViewBlogpostQueryHandler _viewBlogpostQueryHandler)
        {
            this.addBlogpostCommandHandler = _addBlogpostCommandHandler;
            this.viewAllBlogpostsQueryHandler = _viewAllBlogpostsQueryHandler;
            this.viewBlogpostQueryHandler = _viewBlogpostQueryHandler;
        }

        [HttpPost("addBlogpost")]
        //[Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> AddBlogpostAsync([FromBody] AddBlogpostCommand command, CancellationToken cancellationToken)
        {
            await addBlogpostCommandHandler.HandleAsync(command, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet("viewAllBlogposts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BlogpostDto>>> ViewAllBlogpostsAsync(CancellationToken cancellationToken)
        {
            var blogposts = await viewAllBlogpostsQueryHandler.HandleAsync(cancellationToken);

            return Ok(blogposts);
        }

        [HttpGet("viewBlogpost/{id}")]
        [Authorize]
        public async Task<ActionResult<BlogpostsWithCommentsDto>> ViewBlogpostAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var blogpost = await viewBlogpostQueryHandler.HandleAsync(id, cancellationToken);

            return Ok(blogpost);
        }
    }
}
