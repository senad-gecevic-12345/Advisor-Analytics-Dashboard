namespace fullstack_1.Data
{
    public class Advisor : User
    {
        public Advisor(string id, string first_name, string last_name, string login_name, Level level, UserType type, List<Address> addresses, List<ContactNumber> contact_numbers, List<UserExtensions> user_extensions)
        :base(id, first_name, last_name, login_name, level, type, addresses, contact_numbers, user_extensions)
        {
        }
        public Advisor(string id, string first_name, string last_name, string login_name, string email, Level level, UserType type, List<Address> addresses, List<ContactNumber> contact_numbers, List<UserExtensions> user_extensions)
        :base(id, first_name, last_name, login_name, email, level, type, addresses, contact_numbers, user_extensions)
        {
        }
        public Advisor()
        {
        }

    }
    public class AdvisorDTOOUT
    {
        string id { get; set; }
        string first_name { get; set; }
        string last_name { get; set; }
    }
}
