using FriendOrganizer.UI.ViewModel;
using MahApps.Metro.Controls;
using System.Windows;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// The views constructor takes a ViewModel
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            //Store the view model in its own variable
            _viewModel = viewModel;

            // use that vaiable in the DataContext assignment
            DataContext = _viewModel;
            //now assign the viewModel to the Datacontext property of the main window
            // DataContext = viewModel;
            // however we need to call the Load method on the main View Model
            //so that the friends get loaded, but don't caall this in the constructor
            // the constructor should just initialise the object
            //call in the loaded event handler for the main window
            Loaded += MainWindow_Loaded;
        }

       // private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //load the friends
            // _viewModel.Load();
            await _viewModel.LoadAsync();

        }
    }
 }
