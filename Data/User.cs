
// have some sort of array where it has the database name members etc
// then do query to check what type of database it is
namespace fullstack_1.Data
{
    public class Level
    {
        public string level { get; set; }

        public Level(string level)
        {
            this.level = level;
        }
    }
    public class UserType
    {
        public string type { get; set; }
        public UserType(string type)
        {
            this.type = type;
        }
    }

    public class User : Person
    {
        public Level level { get; set; }
        public UserType type { get; set; }
        public List<Address> addresses { get; set; }
        public User(string id, string first_name, string last_name, string login_name, string email, Level level, UserType type, List<Address> addresses, List<ContactNumber> contact_numbers, List<UserExtensions> user_extensions)
        :base(id, first_name, last_name, login_name, email, contact_numbers, user_extensions)
        {
            this.level = level;
            this.type = type;
            this.addresses = addresses;
        }
        public User(string id, string first_name, string last_name, string login_name, Level level, UserType type, List<Address> addresses, List<ContactNumber> contact_numbers, List<UserExtensions> user_extensions)
        :base(id, first_name, last_name, login_name, contact_numbers, user_extensions)
        {
            this.level = level;
            this.type = type;
            this.addresses = addresses;
        }
        public User() 
        : base()
        {
            this.level = new Level("INVALID");
            this.type = new UserType("INVALID");
            this.addresses = new List<Address>();
        }

        public User(string id, UserDTOPost dto)
            :base(id, dto)
        {
            level = dto.level;
            type = dto.type;
            addresses = dto.addresses;
            //email = String.Empty;       
        }
    }

    public class UserDTOPost : PersonDTOPost
    {
        public Level level { get; set; }
        public UserType type { get; set; }
        public List<Address> addresses { get; set; }
        
        public UserDTOPost(string first_name, string last_name, string login_name, string email, Level level, UserType type, List<Address> addresses, List<ContactNumber> contact_numbers, List<UserExtensions> extensions)
            :base(first_name, last_name, login_name, email, contact_numbers, extensions)
        {
            this.level = level;
            this.type = type;
            this.addresses = addresses;
        }
          public UserDTOPost()
            :base()
        {
        }

        /*
        public UserDTOPost(string first_name, string last_name, string login_name, Level level, UserType type)
            :base(first_name, last_name, login_name)
        {
            this.level = level;
            this.type = type;
        }
        */
        
    }

}
