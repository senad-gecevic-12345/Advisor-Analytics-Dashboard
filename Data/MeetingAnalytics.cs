namespace fullstack_1.Data
{
 public class WeeklyStatistics
        {
            // select for this day
            public string day { get; set; }
            // how many meetings per this day
            public int per_day { get; set; }
            // ?????
            public string status { get; set; }
            public WeeklyStatistics() { }
        }

    public class MeetingAnalytics
    {
        public int total_meetings { get; set; }
        public int completed_meetings { get; set; }
        public string average_waiting_time { get; set; }
        public string longest_waiting_time { get; set; }
        // weekly statistics for that week
        public List<WeeklyStatistics> statistics { get; set; }
        // maybe status so that if passes to other statistics can see if it is completed or not
       public MeetingAnalytics() {
            statistics = new List<WeeklyStatistics>();
        }
        public MeetingAnalytics(int total_meetings, int completed_meetings) {
            statistics = new List<WeeklyStatistics>();
        }
    }
}


