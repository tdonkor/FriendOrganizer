using System;
using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>
    {
        public FriendWrapper(Friend model) : base(model)
        {

        }
        public int Id { get { return Model.Id; } }

        public string FirstName
        {
           // get { return Model.FirstName; } - call a method to get value
            get { return GetValue<string>(); }
            set
            {
                // Model.FirstName = value; - use set property 
               SetValue(value);
               //OnPropertyChanged(); Now in set value
              // ValidateProperty(nameof(FirstName));
            }
        }

        public string LastName
        {
            // get { return Model.LastName; }
            get { return GetValue<string>(); }
            set
            {
                //Model.LastName = value;
                //OnPropertyChanged();
                SetValue(value);
            }
        }

        public string Email
        {
           // get { return Model.Email; }
            get { return GetValue<string>(); }
            set
            {
                //Model.Email = value;
                //OnPropertyChanged();
                SetValue(value);
            }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robots are not valid friends";
                    }
                    break;
            }

        }

            //private void ValidateProperty(string propertyName)
            //{
            //    ClearErrors(propertyName);
            //    switch(propertyName)
            //    {
            //        case nameof(FirstName):
            //            if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
            //            {
            //                AddError(propertyName, "Robots are not valid friends" );
            //            }
            //            break;
            //    }
            //}



        }

}
