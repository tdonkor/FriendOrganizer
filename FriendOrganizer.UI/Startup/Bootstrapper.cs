using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;
using FriendOrganizer.UI.View.Services;

namespace FriendOrganizer.UI.Startup
{
    /// <summary>
    /// responsible to create the autofac container
    /// The container knows about all the types
    /// And is used to create instances.
    /// 
    /// ViewModel is automatically injected into the views constructor
    /// and the dataService is injected into the ViewModels constructor
    /// using Dependency Injection
    /// </summary>
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            // The container now knows when an IFriendDataService dataService is required somewhere 
            // it will create an instance of the FriendRepository class
            //you can also register the concrete types that don't implement an interface 
            //if I want to resolve these types from the container

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();
            
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().Keyed<IDetailViewModel>(nameof(FriendDetailViewModel));
            builder.RegisterType<MeetingDetailViewModel>().Keyed<IDetailViewModel>(nameof(MeetingDetailViewModel));
            builder.RegisterType<ProgrammingLanguageDetailViewModel>().Keyed<IDetailViewModel>(nameof(ProgrammingLanguageDetailViewModel));


            builder.RegisterType<LookUpDataService>().AsImplementedInterfaces();
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();
            builder.RegisterType<ProgrammingLanguageRepository>().As<IProgrammingLanguageRepository>();

            //create the container
            return builder.Build();


        }
    }
}
