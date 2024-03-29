﻿using BlogForPeace.Api.Features.Blogposts.AddBlogpost;
using BlogForPeace.Api.Features.Blogposts.ViewAllBlogposts;
using BlogForPeace.Api.Features.Blogposts.ViewBlogpost;
using BlogForPeace.Api.Features.Blogposts.EditBlogpost;

namespace BlogForPeace.Api.Features.Blogposts
{
    internal static class BlogpostsModule
    {
        internal static void AddBlogpostsHandlers(this IServiceCollection services)
        {
            services.AddTransient<IAddBlogpostCommandHandler, AddBlogpostCommandHandler>();
            services.AddTransient<IViewAllBlogpostsQueryHandler, ViewAllBlogpostsQueryHandler>();
            services.AddTransient<IViewBlogpostQueryHandler, ViewBlogpostQueryHandler>();
            services.AddTransient<IEditBlogpostCommandHandler, EditBlogpostCommandHandler>();
        }
    }
}
