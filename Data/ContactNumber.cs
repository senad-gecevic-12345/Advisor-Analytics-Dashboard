namespace fullstack_1.Data
{
    public class ContactNumber
    {
        public string type { get; set; }
        public string value { get; set; }
        public ContactNumber() { }
        
        public ContactNumber(string type, string value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
