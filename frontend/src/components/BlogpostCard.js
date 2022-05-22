import React from "react";
import { BsDot } from "react-icons/bs";
import {Button} from 'antd';

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
          <div>{text}</div>
        </div>
        <div className="location">
          <div>Location: {location}</div>
        </div>
      </div>
      <div className="tags"> 
        tags: 
        {tags.length > 0 &&
          tags.map((tag) => tag.name).join(", ")}
      </div>
        <Button type="text" onClick={() => handleClick({ id })}>
            View
        </Button>
        <br/><br/><br/>
    </div>
  );
};

export default BlogpostCard;
