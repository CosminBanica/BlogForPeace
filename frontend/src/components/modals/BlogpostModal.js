import React, { useState } from "react";
import Modal from "react-modal";
import { MdOutlineClose } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";
import Tags from "../Tags";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";

const BlogpostModal = ({ modalIsOpen, closeModal, submitForm }) => {
  const { register, handleSubmit, getValues } = useForm();
    const [tags, setTags] = useState([]);
    const navigate = useNavigate();

  const handleClick = async () => {
    const data = getValues();
    submitForm({ ...data, tags });
    navigate("/");
  };

  return (
    <Modal
      isOpen={modalIsOpen}
      onRequestClose={closeModal}
      contentLabel="Add blogpost"
      className="modal"
      ariaHideApp={false}
    >
      <div className="row-between">
        <h2>Add blogpost</h2>
        <Button onClick={closeModal} className="icon-button">
          <MdOutlineClose />
        </Button>
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
        {/*<Tags*/}
        {/*  tags={tags}*/}
        {/*  setTags={setTags}*/}
        {/*  label="Tags"*/}
        {/*  placeholder="Press enter to save tag"*/}
        {/*/>*/}
        <Button type="button" onClick={handleSubmit(handleClick)}>
          Add blogpost
        </Button>
      </form>
    </Modal>
  );
};

export default BlogpostModal;
