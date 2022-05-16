using MediatR;

namespace BlogForPeace.Core.Domain.UserComments
{
    public record UserAddedTagEvent
    {
        public string Name { get; private set; }
        public UserAddedTagEvent(string name)
        {
            Name = name;
        }
    }
}
