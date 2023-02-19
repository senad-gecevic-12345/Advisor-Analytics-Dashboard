namespace fullstack_1.Data
{
    public class Company
    {
        public string id { get; set; }
        public string name { get; set; }
        public string industry { get; set; }
        public string location { get; set; }
        public Company(string id, string name, string industry, string location)
        {
            this.id = id;
            this.name = name;
            this.industry = industry;
            this.location = location;
        }
        public Company() { }
    }
}
