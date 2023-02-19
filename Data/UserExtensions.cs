namespace fullstack_1.Data
{
    public class UserExtensions
    {
        public string extension_number { get; set; }
        public List<Did>? dids { get; set; }
        public UserExtensions() { }
        public UserExtensions(string extension_number)
        {
            this.extension_number = extension_number; 
        }

    }
}
