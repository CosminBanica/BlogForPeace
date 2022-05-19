import React, { useMemo, useState, useEffect, useCallback } from "react";
import BookCard from "../../components/BlogpostCard";
//import RentModal from "../../components/modals/RentModal";
import { useNavigate } from "react-router-dom";
import UserLayout from "../../utils/UserLayout";
import axiosInstance from "../../configs/Axios";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../../configs/Api";
import BlogpostCard from "../../components/BlogpostCard";

const UserBlogposts = () => {
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
        let path = "blogposts/" + blogpost.id;
        navigate(path);
    };

    useEffect(() => {
        getAllBlogposts();
    }, [getAllBlogposts]);

    return (
        <UserLayout>
            <div className="blogposts">
                {blogposts.map((blogpost, index) => (
                    <BlogpostCard key={index} {...blogpost} handleClick={handleClick} />
                ))}
            </div>
        </UserLayout>
    );

};

export default UserBlogposts;