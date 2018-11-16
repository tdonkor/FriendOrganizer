using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Startup;
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
            //        new FriendDataService()));
            //mainWindow.Show();

            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var mainWindow





        }
    }
}
