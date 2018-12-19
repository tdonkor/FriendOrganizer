using FriendOrganizer.Model;


namespace FriendOrganizer.UI.Wrapper
{
    public class ProgrammingLanguageWrapper : ModelWrapper<ProgrammingLanguage>
    {
        public ProgrammingLanguageWrapper(ProgrammingLanguage model) : base(model)
        {

        }
        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
