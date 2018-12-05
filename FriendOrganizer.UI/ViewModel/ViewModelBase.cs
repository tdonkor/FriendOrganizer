
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace FriendOrganizer.UI.ViewModel
{
    /// <summary>
    /// Base class that implements INotifyPropertyChanged
    /// we need the propertyChanged  event in different ViewModels throughout the project
    /// so put it in its own seperate base class
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        //Implements the INotifyPropertyChanged  then we can raise this event from the setter
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper method to raise the event
        /// Includes name of the changed property
        /// we can make this optional
        /// To use the method from subclasses make it protected
        /// to make subclasses override it make it virtual
        /// </summary>
        /// <param name="PropertyName"></param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            //check property changed is not null
            // Raise the event by calling the invoke method
            //sender is our main view model i.e this class
            //PropertyChangedEventArgs will contain our changed property name 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
