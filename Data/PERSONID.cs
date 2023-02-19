namespace fullstack_1.Data
{
    public class PersonId
    {
        public string id { get; set; }
        public string table_name { get;set; }
        public PersonId() { }
        public PersonId(string id, string table_name)
        {
            this.id = id;
            this.table_name = table_name;
        }
    }
}
