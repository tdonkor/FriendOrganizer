using System;
using FriendOrganizer.Model;


namespace FriendOrganizer.UI.Wrapper
{
    public class MeetingWrapper : ModelWrapper<Meeting>
    {
        public MeetingWrapper(Meeting model) : base(model)
        {

        }

        public int Id { get { return Model.Id; } }

        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// start date Must be less than End Date
        /// </summary>
        public DateTime DateFrom
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                if (DateTo < DateFrom)
                {
                    DateTo = DateFrom;
                }
                
            }
        }

        /// <summary>
        /// End date must be greater than start date
        /// </summary>
        public DateTime DateTo
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                //end date less than start date
                if (DateTo < DateFrom)
                {
                    DateFrom = DateTo;
                }
               
            }
        }
    }
}
