using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendRepository _friendRepository;
        private IEventAggregator _eventAggregator;
        private bool _hasChanges;

        //was a Friend now a FriendWrapper
        private FriendWrapper _friend;

        public FriendWrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

    

        public bool HasChanges
        {
            get { return _hasChanges; }
            set {
                    if(_hasChanges != value)
                    {
                        _hasChanges = value;
                        OnPropertyChanged();
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }
                
            }
        }


        //don't need the setter as we initialise it directly in the constructor
        // of the friendDetailView Model
        public ICommand SaveCommand{ get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="friendRepository"></param>
        /// <param name="eventAggregator"></param>
        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator)
        {
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;

            //subscribe to the OpenFriendDetailViewEvent in the constructor
           // _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            //Check in addition if friend has changes
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            //with the wrapper now use the Friend.Model instance
            await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(
                new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });

        }



        
        /// <summary>
        ///  A friend in selected in the Navigation the  is notified
        ///  loads the Id of the Friend 
        /// </summary>
        /// <param name = "friendId" ></ param >
        //private async void OnOpenFriendDetailView(int friendId)
        //{
        //    await LoadAsync(friendId);
        //}

        /// <summary>
        /// Loads the friend fom the dataService using the ID
        /// LoadAsync method is called and goes to the Data Service
        /// use a FriendWrapper
        /// </summary>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public async Task LoadAsync(int friendId)
        {
            var friend = await _friendRepository.GetByIdAsync(friendId);

            //use a  FriendWrapper
            Friend = new FriendWrapper(friend);
           
            //use lambda
            Friend.PropertyChanged += (s, e) =>
            {
                if(!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
           
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
