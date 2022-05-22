import React from "react";
import { BsDot } from "react-icons/bs";
import Button from "./Button";

const BlogpostCard = ({
  id,
  title,
  text,
  location,
  tags,
  handleClick,
}) => {
  return (
    <div className={`blogpost-card}`}>
      <div className="blogpost-group">
        <div className="title">
          <h3>{title}</h3>
        </div>
        <div className="text">
          <h3>{text}</h3>
        </div>
        <div className="location">
          <h3>{location}</h3>
        </div>
      </div>
      <div className="tags">
        {tags.length > 0 &&
          tags.map((tag) => tag.name).join(", ")}
      </div>
        <Button onClick={() => handleClick({ id })}>
            View
        </Button>
    </div>
  );
};

export default BlogpostCard;
