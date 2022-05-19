import React, { useState } from "react";
import { MdOutlineClose } from "react-icons/md";

const Tags = (props) => {
  const { label, tags, setTags, ...rest } = props;
  const [value, setValue] = useState("");

  const handleKeyDown = (event) => {
    if (event.key === "Enter" && value.length > 0) {
        setTags((k) => k.concat([{ name: value, description: value }]));
      setValue("");
    }
  };

  const handleRemoveTag = (id) =>
    setTags((tags) => tags.filter((_, index) => index !== id));

  const handleChange = (event) => {
    setValue(event.target.value);
  };

  return (
    <>
      <label>
        {label && <span>{label}</span>}
        <input
          {...rest}
          value={value}
          onChange={handleChange}
          className={`input`}
          onKeyDown={handleKeyDown}
        />
      </label>
      {tags.length > 0 && (
        <div className="tag-list">
          {tags.map((tag, index) => (
            <span key={index} onClick={() => handleRemoveTag(index)}>
              {tag.name} <MdOutlineClose />
            </span>
          ))}
        </div>
      )}
    </>
  );
};

export default Tags;
