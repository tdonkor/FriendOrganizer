using FriendOrganizer.Model;
using System.Collections.Generic;


namespace FriendOrganizer.UI.Data
{
    //call the GetAll method to load in the 
    //friend details
    public interface IFriendDataService
    {
        IEnumerable<Friend> GetAll();
    }
}
