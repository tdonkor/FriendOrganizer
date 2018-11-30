using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FriendOrganizer.UI.ViewModel;

namespace FriendOrganizer.UI.Wrapper
{
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        //use propertyname as a key, store a list of errors
        private Dictionary<string, List<string>> _errorsByPropertyName
            = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName)
                ? _errorsByPropertyName[propertyName]
                : null;
        }

        /// <summary>
        /// helper method to raise the errorschanged event
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnErrorsChanged(string propertyName)
        {
            //Null conditional error checker -check if ErrorsChanged event is not null
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }

        // methods that simplyfy how we can add and remove errors 
        //we recieve an error which we need to store under the property name
        //dictionatry should contain an entry for the property name
        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }

            //check error is not already part of the list
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        //check if the dictionary contains an entry for the propertyname
        //if so remove that entry for that property 
        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }

}
