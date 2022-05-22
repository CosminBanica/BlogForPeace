import React, { useState } from "react";
import Modal from "react-modal";
import { MdOutlineClose } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";
import { useLocation, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import Tags from "../Tags";

const AccountModal = ({ modalIsOpen, closeModal, submitForm }) => {
    const { register, handleSubmit, getValues } = useForm();
    const [tags, setTags] = useState([]);

    const handleClick = async () => {
        const data = getValues();
        submitForm({ ...data, tags });
        closeModal();
    };

    return (
        <Modal
        ariaHideApp={false}
            isOpen={modalIsOpen}
            style={{display: 'block !important'}}
        onRequestClose={closeModal}
        contentLabel="Edit account"
        className="modal"
        >
        <div className="row-between">
        <br/><br/><br/>
        <br/><br/><br/>

        <h2>Edit account</h2>
        <Button onClick={closeModal} className="icon-button">
            <MdOutlineClose />
        </Button>
        </div>
        <div className="line" />
        <form>
        <Input label="Name:" placeholder="Your full name:" {...register("name")} />
        <br></br>
        <Input label="Email:" placeholder="Your email address:" {...register("email")} />
        <br></br>
        <Input label="Address:" placeholder="Your address" {...register("address")} />
        <br></br>
        <Tags
            tags={tags}
            setTags={setTags}
            label="Tags"
            placeholder="Press enter to save tag"
        />
        <Button type="button" onClick={handleSubmit(handleClick)}>
            Save
        </Button>
        </form>
    </Modal>
    );
};

export default AccountModal;
