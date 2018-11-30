using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookUpDataService _friendLookUpService;
        private IEventAggregator _eventAggregator;

        //bound by the navigation view to display the friends
        //Was previously LookupItem
        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        // Now not needed
        //Was previously LookupItem
        //private NavigationItemViewModel _selectedFriend;

        ////Was previously LookupItem
        //public NavigationItemViewModel SelectedFriend
        //{
        //    get { return _selectedFriend; }
        //    set { _selectedFriend = value;
        //        OnPropertyChanged();
        //        if (_selectedFriend!= null)
        //        {
        //            //publish the event when a friend is selected
        //            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(_selectedFriend.Id);
        //        }
        //    }
        //}


        public NavigationViewModel(IFriendLookUpDataService friendLookUpService, IEventAggregator eventAggregator)
        {
            _friendLookUpService = friendLookUpService;
            _eventAggregator = eventAggregator;
            //replace the lookup item
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(AfterFriendSaved);
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs obj)
        {
            var lookupItem  = Friends.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            // friendLookUpService returns a LookUp previously
            var lookUp = await _friendLookUpService.GetFriendLookUpAsync();
            
            //so we can call the loadAsync method multiple times
            Friends.Clear();

            //was previously lookup item
            foreach (var item in lookUp)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }
    }
}
