using Autofac;
using FriendOrganizer.UI.Startup;
using System;
using System.Windows;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Don't use this pass mainViwModel to MainWindow constructor
            //and pass FrienddataService to the Main ViewModel constructor
            //if you change your constructor of one of your classes you have to change
            //this piece of code below, so use dependency injection ibrary autofac which resolves the main window 
            //instance from a container that knows how to create all the dependencies
            //
            //var mainWindow = new MainWindow(
            //    new MainViewModel(
            //        new FriendRepository()));
            //mainWindow.Show();

            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            //goes to the MainWindowContructor to construct a MainView Model, then goes to the MainView Model
            //constuctor and sees that it has to construct an IFriendDataService and because of the info provided
            //has to set up a FriendRepository instance
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();

        }

        private void Application_DispatcherUnhandledException(object sender, 
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occurred. Please inform the admin."
                + Environment.NewLine + e.Exception.Message, "Unexpected error");
            e.Handled = true;
        }
    }
}
