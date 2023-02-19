namespace fullstack_1.Data
{
    public enum EventStatus
    {
        PENDING, 
        CONFIRMED,
        COMPLETED,
        CANCELLED
    }
    public class EventRequest{
        public string date { get; set; } 
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string title { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        // shoudl be string not enum
        public string status { get; set; }
        public EventRequest() { }
        public EventRequest(string date, string start_time, string end_time, string title, int created_at, int updated_at, string status)
        {
            this.date = date;
            this.start_time = start_time;
            this.end_time = end_time;
            this.title = title;
            this.created_at = created_at;
            this.updated_at = updated_at;
            this.status = status;
        }
    }
    public class Event
    {
        public string id { get; set; }
        public string calendar_id { get; set; }
        public EventRequest detail { get; set; }
        public Event(string id, string calendar_id, string date, string start_time, string end_time, string title, int created_at, int updated_at, string status)
        {
            this.id = id;
            this.calendar_id = calendar_id;
            detail = new EventRequest(date, start_time, end_time, title, created_at, updated_at, status);
        }
        public Event() { }
    }
}
