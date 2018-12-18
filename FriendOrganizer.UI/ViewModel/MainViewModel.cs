
using Autofac.Features.Indexed;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private IEventAggregator _eventAggregator;
        private IDetailViewModel _detailViewModel;
        private IMessageDialogService _messageDialogService;
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;

        //private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
       // private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        //private Func<IMeetingDetailViewModel> _meetingDetailViewModelCreator;


        public MainViewModel(INavigationViewModel navigationVewModel,
            //Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            //Func<IMeetingDetailViewModel> meetingDetailViewModelCreator,
            IIndex<string,IDetailViewModel> detailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;
            //_friendDetailViewModelCreator = friendDetailViewModelCreator;
            //_meetingDetailViewModelCreator = meetingDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            NavigationViewModel = navigationVewModel;
        }

        public ICommand CreateNewDetailCommand { get; }
        public INavigationViewModel NavigationViewModel { get; }
        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            private set { _detailViewModel = value;
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

        /// <summary>
        /// Load the friends from the Friends Data Service
        /// </summary>
        //public void Load()
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
            
        }

        //
        /// <summary>
        /// Where we create the detail ViewModel
        /// </summary>
        /// <param name="friendId"></param>
        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            //blocks the Navigatin if the user has made changes to a friend need to do the same for a new friend
            //for a new friend create a new view Model and pass in a null value to the loadAsync method
            if(DetailViewModel!=null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
            
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
               
            }

            DetailViewModel = _detailViewModelCreator[args.ViewModelName];
            await DetailViewModel.LoadAsync(args.Id);

            //switch(args.ViewModelName)
            //{
            //    case nameof(FriendDetailViewModel):
            //    DetailViewModel = _friendDetailViewModelCreator();
            //    break;
            //    case nameof(MeetingDetailViewModel):
            //        DetailViewModel = _meetingDetailViewModelCreator();
            //        break;
            //    default:
            //        throw new Exception($"ViewModel { args.ViewModelName} not mapped");

         
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs { ViewModelName = viewModelType.Name });
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }

    }
}
