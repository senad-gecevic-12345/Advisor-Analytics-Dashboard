namespace fullstack_1.Data
{
    public class Did
    {
        public string extension { get; set; }
        public string custom_tag { get; set; }
        public string phone_number { get; set; }
        public Did() { }
        public Did(string extension, string custom_tag, string phone_number)
        {
            this.extension = extension;
            this.custom_tag = custom_tag;
            this.phone_number = phone_number;
        }
    }
}
