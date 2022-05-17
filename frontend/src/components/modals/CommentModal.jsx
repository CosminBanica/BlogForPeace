import React from "react";
import Modal from "react-modal";
import { MdOutlineClose } from "react-icons/md";
import Button from "../Button";
import Input from "../Input";
import { useForm } from "react-hook-form";

const CommentModal = ({ modalIsOpen, closeModal, blogpost, submitForm }) => {
  const { register, handleSubmit, getValues } = useForm();

  const handleClick = async () => {
    const data = getValues();
    submitForm({ blogpost: blogpost.id, message: data.message });
    closeModal();
  };

  return (
    <Modal
      isOpen={modalIsOpen}
      onRequestClose={closeModal}
      contentLabel="Add blogpost"
      className="modal"
    >
      <div className="row-between">
        <h2>Comment on blogpost</h2>
        <Button onClick={closeModal} className="icon-button">
          <MdOutlineClose />
        </Button>
      </div>
      <div className="line" />
      <form>
        <Input
          label="Blogpost"
          disabled
          placeholder="Blogpost title"
          value={blogpost.title}
        />
        <Input
          label="Message"
          placeholder="Comment message"
          type="number"
          {...register("message")}
        />
        <Button type="button" onClick={handleSubmit(handleClick)}>
          Comment on blogpost
        </Button>
      </form>
    </Modal>
  );
};

export default CommentModal;
