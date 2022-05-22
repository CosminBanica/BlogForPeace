import React, { useMemo, useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../configs/Axios";
import { useAuth0 } from "@auth0/auth0-react";
import { routes } from "../configs/Api";
import BlogpostCard from "../components/BlogpostCard";
import Button from "../components/Button";
import AccountModal from "../components/modals/AccountModal";
import { MdEdit } from "react-icons/md";
import Section from "../components/Section";
import Table from "../components/Table";
import { Descriptions, Badge } from 'antd';
import { List, Typography, Divider } from 'antd';



const Account = () => {
    const { getAccessTokenSilently } = useAuth0();

    const [openedModal, setOpenedModal] = useState(false);
    const [profile, setProfile] = useState({});

    const profileFields = [
        { key: "Name", value: profile.name },
        { key: "Email", value: profile.email },
        { key: "Address", value: profile.address },
    ];

    const columns = [
        {
            Header: "name",
            accessor: "name",
        },
        {
            Header: "description",
            accessor: "description",
        },
    ];

    const handleEditProfile = (form) => {
        (async () => {
            const accessToken = await getAccessTokenSilently();
            axiosInstance
                .put(routes.profile.editProfile, form, {
                    headers: {
                        Authorization: `Bearer ${accessToken}`,
                    },
                }, { timeout: 1000000})
                .then(() => getProfile());
        })();
    };

    const getProfile = useCallback(async () => {
        const accessToken = await getAccessTokenSilently();
        axiosInstance
            .post(routes.profile.setupProfile, null, {
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                }
            }, { timeout: 100000 })
            .then(() => axiosInstance
                .get(routes.profile.getProfile, {
                    headers: {
                        Authorization: `Bearer ${accessToken}`,
                    },
                }, { timeout: 1000000 })
                .then(({ data }) => setProfile(data)));
    }, [getAccessTokenSilently]);

    useEffect(() => {
        getProfile();
    }, [getProfile]);

    return (
        <div>
            <nav className="navbar navbar-light navbar-expand-lg fixed-top bg-white clean-navbar">
                <div className="container"><a className="navbar-brand logo" href="#">BlogForPeace</a><button data-toggle="collapse" className="navbar-toggler" data-target="#navcol-1"><span className="sr-only">Toggle navigation</span><span className="navbar-toggler-icon"></span></button>
                    <div className="collapse navbar-collapse"
                        id="navcol-1">
                        <ul className="nav navbar-nav ml-auto">
                            <li className="nav-item" role="presentation"><a className="nav-link" href="http://localhost:3000/AddBlogpost">Add Post</a></li>
                            <li className="nav-item" role="presentation"><a className="nav-link" href="http://localhost:3000/">Blog</a></li>
                            <li className="nav-item" role="presentation"><a className="nav-link active" href="http://localhost:3000/Account">Edit Profile</a></li>
                        </ul>
                    </div>
                </div>
            </nav>

            <AccountModal
                modalIsOpen={openedModal}
                closeModal={() => {
                    setOpenedModal(false);
                }}
                submitForm={handleEditProfile}
            />

            <br/><br/><br/><br/>

            <Descriptions title="User Info" column={1} layout="vertical" bordered>
                <Descriptions.Item label="Nume:" >{profile.name}</Descriptions.Item>
                <Descriptions.Item label="Email:">{profile.email}</Descriptions.Item>
                <Descriptions.Item label="Address:">{profile.address}</Descriptions.Item>
            </Descriptions>

            <Divider orientation="left"></Divider>
            <List
            size="large"
            header={<div>Lista de taguri:</div>}
            dataSource = {(profile.subscribedTags ?? []).map (x => x.name)}
            bordered
            renderItem={item => <List.Item>{item}</List.Item>}
            />

            <div className="row-between">
                <Button onClick={() => setOpenedModal(true)}>
                    <MdEdit /> Edit
                </Button>
            </div>

        </div>
    );
};

export default Account;