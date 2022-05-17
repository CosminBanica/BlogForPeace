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
        <div className="card-heading">
          <p>{author}</p> <BsDot /> <p>{genre}</p>
        </div>
        <div className="title">
          <h3>{title}</h3>
        </div>
      </div>
      <div className="tags">
        {tags.length > 0 &&
          tags.map((tag) => tags.name).join(", ")}
      </div>
        <Button onClick={() => handleClick({ id })}>
            View
        </Button>
    </div>
  );
};

export default BlogpostCard;
