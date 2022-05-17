import React, { useState, useCallback, useEffect, useMemo } from "react";
import Button from "../../components/Button";
import AccountModal from "../../components/modals/AccountModal";
import UserLayout from "../../utils/UserLayout";
import { MdEdit } from "react-icons/md";
import Section from "../../components/Section";
import Table from "../../components/Table";
import { useAuth0 } from "@auth0/auth0-react";
import axiosInstance from "../../configs/Axios";
import { routes } from "../../configs/Api";

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
            Header: "Name",
            accessor: "tagName",
        },
        {
            Header: "Description",
            accessor: "tagDescription",
        },
    ];

    const getProfile = useCallback(async () => {
        const accessToken = await getAccessTokenSilently();
        axiosInstance
            .get(routes.profile.getProfile, {
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                },
            })
            .then(({ data }) => setProfile(data));
    }, [getAccessTokenSilently]);

    useEffect(() => {
        getProfile();
    }, [getProfile]);

    return (
        <UserLayout>
            <AccountModal
                modalIsOpen={openedModal}
                closeModal={() => {
                    setOpenedModal(false);
                }}
            />
            <div className="row-between">
                <h2>Jake Markel</h2>
                <Button onClick={() => setOpenedModal(true)}>
                    <MdEdit /> Edit
                </Button>
            </div>
            <div className="flex flex-col gap-10">
                <Section title={"Profile Details"} fields={profileFields} />
                <div className="flex flex-col gap-5 w-full p-[1px]">
                    <p className="section-title">Subscribed tags</p>
                    <Table data={profile.tags ?? []} columns={columns} noHref />
                </div>
            </div>
        </UserLayout>
    );
};

export default Account;
