using Autofac;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModel;

namespace FriendOrganizer.UI.Startup
{
    /// <summary>
    /// responsible to create the autofac container
    /// The container knows about all the types
    /// And is used to create instances
    /// </summary>
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            // The container now knows when an IFriendDataService dataService is required somewhere 
            // it will create an instance of the FriendDataService class
            //you can also register the concrete types that don't implement an interface 
            //if I want to resolve these types from the container
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();

            //create the container
            return builder.Build();


        }
    }
}
