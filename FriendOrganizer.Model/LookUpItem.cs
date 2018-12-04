

namespace FriendOrganizer.Model
{
    /// <summary>
    /// Look up dataservice will return this Lookupitem class
    /// </summary>
    public class LookupItem
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }

    public class NullLookupItem : LookupItem
    {
        public new int? Id { get { return null;  } }
    }
}
