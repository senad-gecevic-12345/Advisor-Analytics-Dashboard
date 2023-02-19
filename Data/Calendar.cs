namespace fullstack_1.Data
{
    public class Calendar
    {
        public string id { get; set; }
        public string calendar_name { get; set; }
        public int event_count { get; set; }
        public Calendar(string id, string calendar_name, int event_count)
        {
            this.id = id;
            this.calendar_name = calendar_name;
            this.event_count = event_count;
        }   
        public Calendar() { }
    }
}
