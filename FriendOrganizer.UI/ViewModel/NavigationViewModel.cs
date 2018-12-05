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
        public ObservableCollection<NavigationItemViewModel> Friends { get; }

       
        public NavigationViewModel(IFriendLookUpDataService friendLookUpService, IEventAggregator eventAggregator)
        {
            _friendLookUpService = friendLookUpService;
            _eventAggregator = eventAggregator;
            //replace the lookup item
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
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
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, 
                    _eventAggregator, nameof(FriendDetailViewModel)));
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):

                    var friend = Friends.SingleOrDefault(f => f.Id == args.Id);
                    if (friend != null)
                    {
                        Friends.Remove(friend);
                    }
                    break;
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs obj)
        {
            switch (obj.ViewModelName)
            {
                case nameof(FriendDetailViewModel):

                    var lookupItem = Friends.SingleOrDefault(l => l.Id == obj.Id);
                    if (lookupItem == null)
                    {
                        Friends.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember,
                        _eventAggregator, nameof(FriendDetailViewModel)));
                    }
                    else
                    {
                        lookupItem.DisplayMember = obj.DisplayMember;
                    }
                    break;
            }
        }


    }
}
