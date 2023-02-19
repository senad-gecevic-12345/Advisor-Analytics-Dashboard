using fullstack_1.Data;
using fullstack_1.Status;
namespace fullstack_1.Processing
{
    public class MeetingsProcessing
    {


        

        static string time_span(TimeSpan ts)
        {
            int total = ts.Days;
            int years = total / 365;
            int days = total - years * 365;
            int hours = ts.Hours;
            return years + " years " + days + " days " + hours + " hours";
        }


        static bool status_processing(string current, string status)
        {
            return false;
        }
        static bool same_week(DateTime d1, DateTime d2)
        {
            return ((d2.Date - d1.Date).Days < 7 && (d2.DayOfWeek <= d1.DayOfWeek || d1.DayOfWeek == DayOfWeek.Sunday));
        }
        static bool same_day(DateTime d1, DateTime d2)
        {
            return d1.Date == d2.Date;
        }

        static public Tuple<QueryStatus, MeetingAnalytics> process_statistics_acs_date_order(List<Meeting> list)
        {
            MeetingAnalytics meetings_analytics = new MeetingAnalytics(0, 0);

            TimeSpan total_wait_time = TimeSpan.Zero; 
            DateTime date = DateTime.Parse(list[0].date);
            DateTime last_date = date;

            Func<WeeklyStatistics, WeeklyStatistics> add_day = day =>
            {
                meetings_analytics.statistics.Add(day);
                return meetings_analytics.statistics.Last();
            };

            WeeklyStatistics weekly_statistics_ptr = add_day(new WeeklyStatistics());
            for(int i = 0; i < list.Count; ++i)
            {
                date = DateTime.Parse(list[i].date);
                if(!same_day(date, last_date))
                {
                    weekly_statistics_ptr = add_day(new WeeklyStatistics());
                }

                if (list[i].status == "COMPLETED")
                    ++meetings_analytics.completed_meetings;

                ++meetings_analytics.total_meetings;
                total_wait_time += (date - last_date);

                weekly_statistics_ptr.day = date.Date.ToString();
                weekly_statistics_ptr.status = list[i].status;
                ++weekly_statistics_ptr.per_day;

                last_date = date;
            }


            if(meetings_analytics.statistics.Count > 0)
                meetings_analytics.average_waiting_time = (total_wait_time / meetings_analytics.statistics.Count).ToString();

            return Tuple.Create(QueryStatus.SUCCESS, meetings_analytics);
        }

        static public Tuple<QueryStatus, List<MeetingAnalytics>> process_statistics_acs_date_order_2(List<Meeting> list)
        {
            List<MeetingAnalytics> stat_per_week = new List<MeetingAnalytics>();
            MeetingAnalytics meetings_analytics = new MeetingAnalytics(0, 0);
            WeeklyStatistics weekly_statistics = new WeeklyStatistics();

            TimeSpan wait_time = TimeSpan.Zero; 

            DateTime week_start = DateTime.Parse(list[0].date);
            DateTime date = week_start;
            DateTime prev_date_or_date = date;

            for(int i = 0; i < list.Count; ++i)
            {
                date = DateTime.Parse(list[i].date);
                 if (!same_day(date, prev_date_or_date)) 
                 {
                    meetings_analytics.statistics.Add(weekly_statistics);
                    weekly_statistics = new WeeklyStatistics();
                 }
                if (!same_week(week_start, date))
                {
                    week_start = new DateTime();
                    week_start = date;
                    meetings_analytics.average_waiting_time = (wait_time / meetings_analytics.statistics.Count).ToString();
                    stat_per_week.Add(meetings_analytics);
                    meetings_analytics = new();
                    prev_date_or_date = date;
                    wait_time = TimeSpan.Zero;
                }
                   if (list[i].status == "COMPLETED")
                    ++meetings_analytics.completed_meetings;

                ++meetings_analytics.total_meetings;

                wait_time += (date - prev_date_or_date);
                prev_date_or_date = date;
                weekly_statistics.day = date.Date.ToString();
                weekly_statistics.status = list[i].status;
                ++weekly_statistics.per_day;
            }
            if(same_week(week_start, date)) 
            {
                meetings_analytics.statistics.Add(weekly_statistics);
                meetings_analytics.average_waiting_time = (wait_time / meetings_analytics.statistics.Count).ToString();
                stat_per_week.Add(meetings_analytics);
            }

            return Tuple.Create(
                QueryStatus.SUCCESS, stat_per_week);
        }
    }
}
