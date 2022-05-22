import React, { useMemo, useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../configs/Axios";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import BlogpostCard from "../components/BlogpostCard";

const Blogposts = () => {
    const { getAccessTokenSilently } = useAuth0();

    const [blogposts, setBlogposts] = useState([]);
    const navigate = useNavigate();

    const getAllBlogposts = useCallback(async () => {
        const accessToken = await getAccessTokenSilently();
        axiosInstance
            .get(routes.blogposts.getAll, {
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                },
            })
            .then(({ data }) => setBlogposts(data));
    }, [getAccessTokenSilently]);

    const handleClick = (blogpost) => {
        let path = "Blogpost/" + blogpost.id;
        navigate(path);
    };

    useEffect(() => {
        getAllBlogposts();
    }, [getAllBlogposts]);

    return (
        <div>
            <nav className="navbar navbar-light navbar-expand-lg fixed-top bg-white clean-navbar">
                <div className="container"><a className="navbar-brand logo" href="#">BlogForPace</a><button data-toggle="collapse" className="navbar-toggler" data-target="#navcol-1"><span className="sr-only">Toggle navigation</span><span className="navbar-toggler-icon"></span></button>
                    <div className="collapse navbar-collapse"
                        id="navcol-1">
                        <ul className="nav navbar-nav ml-auto">
                            <li className="nav-item" role="presentation"><a className="nav-link" href="http://localhost:3000/AddBlogpost">Add Post</a></li>
                            <li className="nav-item" role="presentation"><a className="nav-link active" href="http://localhost:3000/">Blog</a></li>
                            <li className="nav-item" role="presentation"><a className="nav-link" href="http://localhost:3000/Account">Edit Profile</a></li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div className="blogposts">
                {blogposts.map((blogpost, index) => (
                    <BlogpostCard key={index} {...blogpost} handleClick={handleClick} />
                ))}
            </div>
        </div>
    );
};

export default Blogposts;