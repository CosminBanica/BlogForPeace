import React, { useMemo, useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../configs/Axios";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import BlogpostCard from "../components/BlogpostCard";
import Button from "../components/Button";
import { MdEdit } from "react-icons/md";
import { useForm } from "react-hook-form";
import Section from "../components/Section";
import Table from "../components/Table";
import BlogpostModal from "../components/modals/BlogpostModal";
import Modal from "react-modal";
import Input from "../components/Input";
import Tags from "../components/Tags";

const AddBlogpost = () => {
    const { getAccessTokenSilently } = useAuth0();
    const { register, handleSubmit, getValues } = useForm();
    const [tags, setTags] = useState([]);
    const navigate = useNavigate();

    /*const [openedModal, setOpenedModal] = useState(false);*/
    const [blogpost, setBlogpost] = useState({});

    const columns = [
        {
            Header: "title",
            accessor: "title",
        },
        {
            Header: "text",
            accessor: "text",
        },
        {
            Header: "location",
            accessor: "location",
        },
    ];

    const handleAddBlogpost = (form) => {
        (async () => {
            const accessToken = await getAccessTokenSilently();
            axiosInstance
                .post(routes.blogposts.addBlogpost, form, {
                    headers: {
                        Authorization: `Bearer ${accessToken}`,
                    },
                }, { timeout: 1000000 });
        })();
    };

    const handleClick = async () => {
        const data = getValues();
        handleAddBlogpost({ ...data, tags });
        navigate("/");
    };

    return (
        <div>
            {/*<nav className="navbar navbar-light navbar-expand-lg fixed-top bg-white clean-navbar">*/}
            {/*    <div className="container"><a className="navbar-brand logo" href="#">BlogForPeace</a><button data-toggle="collapse" className="navbar-toggler" data-target="#navcol-1"><span className="sr-only">Toggle navigation</span><span className="navbar-toggler-icon"></span></button>*/}
            {/*        <div className="collapse navbar-collapse"*/}
            {/*            id="navcol-1">*/}
            {/*            <ul className="nav navbar-nav ml-auto">*/}
            {/*                <li className="nav-item" role="presentation"><a className="nav-link active" href="http://127.0.0.1:3000/AddBlogpost">Add Post</a></li>*/}
            {/*                <li className="nav-item" role="presentation"><a className="nav-link" href="http://127.0.0.1:3000/">Blog</a></li>*/}
            {/*                <li className="nav-item" role="presentation"><a className="nav-link" href="http://127.0.0.1:3000/Account">Edit Profile</a></li>*/}
            {/*            </ul>*/}
            {/*        </div>*/}
            {/*    </div>*/}
            {/*</nav>*/}

            <div className="row-between">
                <h2>Add blogpost</h2>
            </div>
            <div className="line" />
            <form>
                <Input label="Title" placeholder="Blogpost title" {...register("title")} />
                <Input label="Text" placeholder="Blogpost text" {...register("text")} />
                <Input
                    label="Location"
                    placeholder="Relevant location"
                    {...register("location")}
                />
                <Tags
                  tags={tags}
                  setTags={setTags}
                  label="Tags"
                  placeholder="Press enter to save tag"
                />
                <Button type="button" onClick={handleSubmit(handleClick)}>
                    Add blogpost
                </Button>
            </form>
            {/*<Button onClick={() => setOpenedModal(true)}>*/}
            {/*    <MdEdit /> Edit*/}
            {/*</Button>*/}
        </div>
    );
}

export default AddBlogpost;