﻿namespace BlogForPeace.Core.DataModel
{
    public record Tags
    {
        public Tags(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; init; }
        public string Description { get; init; }
    }
}
