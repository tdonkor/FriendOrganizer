using FriendOrganizer.Model;
using System.Collections.Generic;


namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            //TODO Load data from a real database
            yield return new Friend { FirstName = "Thomas", LastName = "Huber" };
            yield return new Friend { FirstName = "Andreas", LastName = "Boehler" };
            yield return new Friend { FirstName = "Julia", LastName = "Huber" };
            yield return new Friend { FirstName = "Chrisi", LastName = "Egin" };
        }
    }
}
