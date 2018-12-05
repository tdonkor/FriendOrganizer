
using FriendOrganizer.UI.Event;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;
        private IEventAggregator _eventAggregator;


        public NavigationItemViewModel(int id, string displayMember, 
                                       IEventAggregator eventAggregator,
                                       string detailViewModelName)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            _detailViewModelName = detailViewModelName;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

       

        public int Id { get; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        private string _detailViewModelName;

        public ICommand OpenDetailViewCommand { get;  }

        private void OnOpenDetailViewExecute()
        {
            //publish the event when a friend is selected
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(
                new OpenDetailViewEventArgs
                {
                    Id = Id,
                    ViewModelName = _detailViewModelName
                });
        }

    }
}
