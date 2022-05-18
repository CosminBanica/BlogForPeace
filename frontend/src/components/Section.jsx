import React from "react";

const Section = ({ title, fields }) => {
  return (
    <div className="section-container">
      <p className="section-title">{title}</p>
      <div className="fields">
        {fields &&
          fields.map((field) => (
            <span key={field.key}>
              <span  className="field-key">{field.key}</span>
              <span>{field.value}</span>
            </span>
          ))}
      </div>
    </div>
  );
};

export default Section;
