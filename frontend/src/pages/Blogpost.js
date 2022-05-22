import React, { useMemo, useState, useEffect, useCallback } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import axiosInstance from "../configs/Axios";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import BlogpostCard from "../components/BlogpostCard";
import Section from "../components/Section";
import Table from "../components/Table";

const Blogpost = () => {
    const { getAccessTokenSilently } = useAuth0();
    const { pathname } = useLocation();
    const id = pathname.split("/").reverse()[0];
    const [blogpostInfo, setData] = useState({});

    const blogpostFields = useMemo(
        () => [
            { key: "Title", value: blogpostInfo.title },
            { key: "Text", value: blogpostInfo.text },
            { key: "Location", value: blogpostInfo.location },
            {
                key: "Tags",
                value:
                    blogpostInfo.tags &&
                    blogpostInfo.tags.map((key) => key.name).join(", "),
            },
            {
                key: "Comments",
                value:
                    blogpostInfo.comments &&
                    blogpostInfo.comments.map((key) => key.message).join(", "),
            },
        ],
        [blogpostInfo]
    );


    const getBlogpost = useCallback(async () => {
        const accessToken = await getAccessTokenSilently();
        axiosInstance
            .get(routes.blogposts.getBlogpost(id), {
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                },
            })
            .then(({ data }) => setData(data));
    }, [getAccessTokenSilently, id]);

    useEffect(() => {
        getBlogpost();
    }, [getBlogpost]);

    return (
        <div>
            <nav className="navbar navbar-light navbar-expand-lg fixed-top bg-white clean-navbar">
                <div className="container"><a className="navbar-brand logo" href="#">BlogForPace</a><button data-toggle="collapse" className="navbar-toggler" data-target="#navcol-1"><span className="sr-only">Toggle navigation</span><span className="navbar-toggler-icon"></span></button>
                    <div className="collapse navbar-collapse"
                        id="navcol-1">
                        <ul className="nav navbar-nav ml-auto">
                            <li className="nav-item" role="presentation"><a className="nav-link" href="http://127.0.0.1:3000/AddBlogpost">Add Post</a></li>
                            <li className="nav-item" role="presentation"><a className="nav-link" href="http://127.0.0.1:3000/">Blog</a></li>
                            <li className="nav-item" role="presentation"><a className="nav-link" href="http://127.0.0.1:3000/Account">Edit Profile</a></li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div className="row-between">
                <h2>{blogpostInfo.name}</h2>
            </div>
            <div className="flex flex-col gap-10">
                <Section title={"Blogpost Details"} fields={blogpostFields} />
            </div>
        </div>
    );
};

export default Blogpost;