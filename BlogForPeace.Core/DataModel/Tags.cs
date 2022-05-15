using BlogForPeace.Core.SeedWork;

namespace BlogForPeace.Core.DataModel
{
    public class Tags : Entity
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
