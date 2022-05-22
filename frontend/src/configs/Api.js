const base = "http://blogforpeace:80/api/v1/";
const routes = {
    blogposts: {
        addBlogpost: "Blogposts/addBlogpost",
        getAll: "Blogposts/viewAllBlogposts",
        getBlogpost: (id) => `Blogposts/viewBlogpost/${id}`,
        editBlogpost: (id) => `Blogposts/editBlogpost`,
    },
    comments: {
        addComment: "Comments/addComment",
        likeComment: (id) => `Comments/likeComment/${id}`,
        dislikeComment: (id) => `Comments/dislikeComment/${id}`,
    },
    profile: {
        setupProfile: "Profiles/registerProfile",
        getProfile: "Profiles/viewProfile",
        editProfile: "Profiles/editProfile",
    },
};

export { base, routes };
