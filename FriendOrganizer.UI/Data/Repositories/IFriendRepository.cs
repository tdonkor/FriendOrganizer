﻿using FriendOrganizer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    //call the GetAll method to load in the 
    //friend details
    public interface IFriendRepository
    {
        Task<Friend> GetByIdAsync(int friendId);
        Task SaveAsync();
        bool HasChanges();
        void Add(Friend friend);
        void Remove(Friend model);
        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}
