using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.ViewModel
{
    /// <summary>
    /// Main view Model acts as the link between the view and the Model
    /// links to the View via the DataContext
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IFriendDataService _friendDataService;
        private Friend _selectedFriend;

        
        // in the required property - create a helper method to raise the event as we will have more 
        //properties in the future.
        //Don't need this now as we have it in the base class
        //public event PropertyChangedEventHandler PropertyChanged;

        // ObservableCollection notifies the Databinding when the collection has changed
        // it implements INotifyCollectionChanged which notifies the databinding about changes
        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = friendDataService;
        }

        /// <summary>
        /// Load the friends from the Friends Data Service
        /// </summary>
        public void Load()
        {
            //Gets all the friends from the data Service
            //implement the IFriendDataService interface
            var friends = _friendDataService.GetAll();

            //To ensure that the load method can be called multiple times
            //so as not to have duplicates
            Friends.Clear();

            //get all the friends from friends
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        public ObservableCollection<Friend> Friends { get; set; }

       
        //when the selected friend property is set we need to notify the databinding
        //that it has changed, so we need to raise a propertyChanged event in the setter
        //so they can update the UI accordingly.
        //we need to implement the Interface InotifyPropertyChanged in the main class
        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            { _selectedFriend = value;
                //for c# 6.0 can use  OnPropertyChanged(nameof(SelectedFriend);
                //using the CallerMemberName and the optional means we can then call 
                //OnPropertyChanged without a parameter the compiler will then sort it out
                // OnPropertyChanged("SelectedFriend");
                OnPropertyChanged();
            }
        }

    }
}
