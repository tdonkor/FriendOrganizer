using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    public class AfterFriendSavedEvent : PubSubEvent<AfterFriendSavedEventArgs>
    {
    }

    public class AfterFriendSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
