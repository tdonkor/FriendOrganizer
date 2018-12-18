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
        private IMeetingLookUpDataService _meetingLookUpService;
        private IEventAggregator _eventAggregator;

        //bound by the navigation view to display the friends
        public ObservableCollection<NavigationItemViewModel> Friends { get; }
        public ObservableCollection<NavigationItemViewModel> Meetings { get; }


        public NavigationViewModel(IFriendLookUpDataService friendLookUpService, 
            IMeetingLookUpDataService meetingLookUpService, IEventAggregator eventAggregator)
        {
            _friendLookUpService = friendLookUpService;
            _meetingLookUpService = meetingLookUpService;
            _eventAggregator = eventAggregator;
            
            Friends = new ObservableCollection<NavigationItemViewModel>();
            Meetings = new ObservableCollection<NavigationItemViewModel>();

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

            // Meeting LookUpService returns a LookUp previously
            lookUp = await _meetingLookUpService.GetMeetingLookUpAsync();

            //so we can call the loadAsync method multiple times
            Meetings.Clear();

            //was previously lookup item
            foreach (var item in lookUp)
            {
                Meetings.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    _eventAggregator, nameof(MeetingDetailViewModel)));
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailDeleted(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(Meetings, args);
                    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailSaved(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, args);
                    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
                _eventAggregator, args.ViewModelName));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }

        }
    }
}
