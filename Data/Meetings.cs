namespace fullstack_1.Data
{
    public class Meeting
    {
        public string meetingId { get; set; }
        public class _Advisor
        {
            public string id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public _Advisor(string id, string first_name, string last_name)
            {
                this.id = id;
                this.first_name = first_name;
                this.last_name = last_name;
            }
        }

        _Advisor advisor;
        
        public string founderId { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public Meeting() { }
        public Meeting(string meetingId, string id, string first_name, string last_name, string founderId, string date, string status)
        {
            this.meetingId = meetingId;
            this.advisor = new _Advisor(id, first_name, last_name);
            this.founderId = founderId;
            this.date = date;
            this.status = status;
        }
    }
}
