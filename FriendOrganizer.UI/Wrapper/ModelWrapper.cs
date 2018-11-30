using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.Wrapper
{
    //base class make it generic
    public class ModelWrapper<T> : NotifyDataErrorInfoBase
    {
        //Make it generic
        public ModelWrapper(T model)
        {
            Model = model;
        }

        public T Model { get; }
        //using reflection

        //change to TValue otherwise it would collide with the generic type of the ModelWrapper class
        protected virtual TValue GetValue<TValue>([CallerMemberName]string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        //when a property is set this method is called
        //change to TValue otherwise it would collide with the generic type of the ModelWrapper class
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName]string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName, value);
        }

        //called everytime a property is set with the setValue method
        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            //clear the errors
            ClearErrors(propertyName);

            //1. Validate data Annotations value - nees Validator, ValidationContext and ValidationResult
            ValidateDataAnnotations(propertyName, currentValue);

            //2. Validate Custom Errors
            ValidateCustomErrors(propertyName);
        }

        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            //set the MemberName property to the propertyName 
            var context = new ValidationContext(Model) { MemberName = propertyName };
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(currentValue, context, results);

            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

        private void ValidateCustomErrors(string propertyName)
        {
            //grab the errors
            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        //we can define the validation in a subclass
        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }

}
