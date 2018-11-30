
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    /// <summary>
    /// Main view Model acts as the link between the view and the Model
    /// links to the View via the DataContext
    ///The ViewModel Constructor takes in a data Service i.e  FriendRepository
    /// We subscribe to the the open friend detail view event
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        //Now not needed with NavigationViewModel
        //private IFriendDataService _friendDataService;
        //private Friend _selectedFriend;

        //allows us to create a new friend view model for every friend event
     
        private IEventAggregator _eventAggregator;
        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;
        private IFriendDetailViewModel _friendDetailViewModel;

        public INavigationViewModel NavigationViewModel { get; }
      

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            private set { _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        // in the required property - create a helper method to raise the event as we will have more 
        //properties in the future.
        //Don't need this now as we have it in the base class
        //public event PropertyChangedEventHandler PropertyChanged;

        // ObservableCollection notifies the Databinding when the collection has changed
        // it implements INotifyCollectionChanged which notifies the databinding about changes
        // public MainViewModel(IFriendDataService friendDataService)

        public MainViewModel(INavigationViewModel navigationVewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

            NavigationViewModel = navigationVewModel;

            //Friends = new ObservableCollection<Friend>();
            //_friendDataService = friendDataService;
        }


        /// <summary>
        /// Load the friends from the Friends Data Service
        /// </summary>
        //public void Load()
        public async Task LoadAsync()
        {

            await NavigationViewModel.LoadAsync();
            
            //Now not needed with NavigationViewModel

            //Gets all the friends from the data Service
            //implement the IFriendDataService interface
            //var friends = _friendDataService.GetAll();
           // var friends = await _friendDataService.GetAllAsync();

            //To ensure that the load method can be called multiple times
            //so as not to have duplicates
           // Friends.Clear();

            //get all the friends from friends
            //foreach (var friend in friends)
            //{
            //    Friends.Add(friend);
            //}
        }

        //Now not needed with NavigationViewModel
        // public ObservableCollection<Friend> Friends { get; set; }


        //Now not needed with NavigationViewModel
        //when the selected friend property is set we need to notify the databinding
        //that it has changed, so we need to raise a propertyChanged event in the setter
        //so they can update the UI accordingly.
        //we need to implement the Interface InotifyPropertyChanged in the main class
        //public Friend SelectedFriend
        //{
        //    get { return _selectedFriend; }
        //    set
        //    { _selectedFriend = value;
        //        //for c# 6.0 can use  OnPropertyChanged(nameof(SelectedFriend);
        //        //using the CallerMemberName and the optional means we can then call 
        //        //OnPropertyChanged without a parameter the compiler will then sort it out
        //        // OnPropertyChanged("SelectedFriend");
        //        OnPropertyChanged();
        //    }
        //}

        //
        /// <summary>
        ///  A friend in selected in the Navigation the  is notified
        ///  loads the Id of the Friend 
        /// </summary>
        /// <param name="friendId"></param>
        private async void OnOpenFriendDetailView(int friendId)
        {
            //blocks the Navigatin if the user has made changes to a friend
            if(FriendDetailViewModel!=null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
            
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }

    }
}
