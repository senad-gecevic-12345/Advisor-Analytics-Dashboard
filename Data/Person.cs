namespace fullstack_1.Data
{
    public class Person 
    {
        public List<ContactNumber> contact_number { get; set; }
        public List<UserExtensions> extensions { get; set; }
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string login_name { get; set; }
        public string email { get; set; }
        public Person(string id, PersonDTOPost dto)
        {
            this.id = id;
            contact_number = dto.contact_number;
            extensions = dto.extensions;
            first_name = dto.first_name;
            last_name = dto.last_name;
            login_name = dto.login_name;
            email = dto.email;
        }
        public Person() {
            id = "INVALID";
            first_name = "INVALID";
            last_name = "INVALID";
            login_name = "INVALID";
            email = "INVALID";
            contact_number = new List<ContactNumber>();
            extensions = new List<UserExtensions>();
        }
        public Person(string id, string first_name, string last_name, string login_name, List<ContactNumber> contact_numbers, List<UserExtensions> extensions)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.login_name = login_name;
            this.contact_number = contact_number;
            this.extensions = extensions;
        }
        public Person(string id, string first_name, string last_name, string login_name, string email, List<ContactNumber> contact_numbers, List<UserExtensions> extensions)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.login_name = login_name;
            this.contact_number = contact_number;
            this.extensions = extensions;
            this.email = email;
        }
    }


    public class PersonDTOPost
    {
        public List<ContactNumber> contact_number { get; set; }
        public List<UserExtensions> extensions { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string login_name { get; set; }
        public string email { get; set; }
        /*
        public PersonDTOPost(string first_name, string last_name, string login_name)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.login_name = login_name;
        }       
        */
        public PersonDTOPost(string first_name, string last_name, string login_name, string email, List<ContactNumber> contact_number, List<UserExtensions> extensions)

        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.login_name = login_name;
            this.contact_number = contact_number;
            this.extensions = extensions;
            this.email = email;
        }
       public PersonDTOPost()
       {
       }
 
    }

}
