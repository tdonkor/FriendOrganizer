using FriendOrganizer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Lookups
{
    public interface IMeetingLookUpDataService
    {
        Task<List<LookupItem>> GetMeetingLookUpAsync();
    }
}
