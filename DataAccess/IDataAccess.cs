/*
using fullstack_1.Data;
using fullstack_1.Status;
namespace fullstack_1.DataAccess
{
    public interface IDataAccess
    {

        QueryStatus create_person_id(string id, string table);

        Tuple<QueryStatus, List<User>> get_users();
        Tuple<QueryStatus, User> get_user(string id);
        QueryStatus registrer_user(UserDTOPost dto);
        QueryStatus delete_user(string id);

        Tuple<QueryStatus, List<Founder>> get_founders();
        Tuple<QueryStatus, Founder> get_founder(string id);

        Tuple<QueryStatus, List<Advisor>> get_advisors();
        Tuple<QueryStatus, Advisor> get_advisor(string id);

        Tuple<QueryStatus, List<Event>> get_events_for_calendar(string id);

        Tuple<QueryStatus, List<Calendar>> get_calendars();
        Tuple<QueryStatus, Calendar> get_calendar(string id);

        Tuple<QueryStatus, List<Address>> get_addresses();
        Tuple<QueryStatus, List<Address>> get_addresses_for_user(string id);
        QueryStatus post_address(Address address);

        Tuple<QueryStatus, List<Meeting>> get_meetings();
        Tuple<QueryStatus, List<Meeting>> get_latest_meetings();

        Tuple<QueryStatus, MeetingAnalytics> get_analytics();


    }
}
*/
