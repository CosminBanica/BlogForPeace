namespace BlogForPeace.Api.Features.Blogposts.EditBlogpost
{
    public interface IEditBlogpostCommandHandler
    {
        public Task HandleAsync(EditBlogpostCommand command, CancellationToken cancellationToken);
    }
}
