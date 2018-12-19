using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private bool _hasChanges;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IMessageDialogService MessageDialogService;
        private int _id;
        private string _title;

        public DetailViewModelBase(IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            EventAggregator = eventAggregator;
            MessageDialogService = messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            CloseDetailViewCommand = new DelegateCommand(OnCloseDetailViewExecute);
        }

      

        public abstract Task LoadAsync(int id);
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CloseDetailViewCommand { get; }

        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            protected set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        public bool HasChanges
        {
            get { return _hasChanges; }
            set {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
              }
            }

        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();
        protected abstract void OnSaveExecute();
        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(
                new AfterDetailDeletedEventArgs
                {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = modelId,
                    DisplayMember = displayMember,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseCollectionSavedEvent()
        {
            EventAggregator.GetEvent<AfterCollectionSavedEvent>().Publish(
                new AfterCollectionSavedEventArgs
                {
                    ViewModelName = this.GetType().Name
                });
        }


        protected virtual void OnCloseDetailViewExecute()
        {
            if (HasChanges)
            {
                var result = MessageDialogService.ShowOkCancelDialog(
                    "You've made changes. Close this item?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            EventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Publish(new AfterDetailClosedEventArgs
                {
                    Id = this.Id,
                    ViewModelName = this.GetType().Name
                });
        }

    }
}
