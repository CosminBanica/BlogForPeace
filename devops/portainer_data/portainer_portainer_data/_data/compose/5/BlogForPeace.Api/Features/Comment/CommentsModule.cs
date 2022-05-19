using BlogForPeace.Api.Features.Comment.AddComment;
using BlogForPeace.Api.Features.Comment.DislikeComment;
using BlogForPeace.Api.Features.Comment.LikeComment;

namespace BlogForPeace.Api.Features.Comment
{
    internal static class CommentsModule
    {
        internal static void AddCommentsHandlers(this IServiceCollection services)
        {
            services.AddTransient<IAddCommentCommandHandler, AddCommentCommandHandler>();
            services.AddTransient<IDislikeCommentCommandHandler, DislikeCommentCommandHandler>();
            services.AddTransient<ILikeCommentCommandHandler, LikeCommentCommandHandler>();
        }
    }
}
