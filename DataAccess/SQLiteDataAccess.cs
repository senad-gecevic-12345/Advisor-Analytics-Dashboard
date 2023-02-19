/*
using fullstack_1.Data;
using fullstack_1.Status;
using fullstack_1.Database;
using fullstack_1.Processing;


// handler calls the functions here
// and  these calls are not calling other functions
// then for things such as register person id then user, etc. 
// that logic goes at the handler

namespace fullstack_1.DataAccess
{
    public class SQLiteDataAccess : IDataAccess
    {

        public QueryStatus create_person_id(string id, string table)
        {
            return Query<PersonId>.post_query("INSERT INTO PERSONID(Id, TableName) " +
                                              "VALUES(@Id, @TableName);",
                                              p =>
                                              {
                                              p.AddWithValue("@Id", id);
                                              p.AddWithValue("@TableName", table);
                                              });
        }

        public QueryStatus delete_user(string id)
        {
            return Query<PersonId>.delete_query("DELETE FROM PERSONID WHERE Id = @Id",
                p =>
                {
                    p.AddWithValue("@Id", id);
                });

        }

        public Tuple<QueryStatus, List<Address>> get_addresses()
        {
            return Query<Address>.read_query("SELECT Street, Number, City, Country, PostCode FROM ADDRESS",
                    reader =>
                    {
                        return new Address(
                            DB.string_null_check(in reader, 0),
                            DB.int_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4));
                    }
                    );
        }

        public Tuple<QueryStatus, List<Address>> get_addresses_for_user(string id)
        {
            return Query<Address>.read_query("SELECT Street, Number, City, Country, PostCode FROM ADDRESS WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new Address(
                            DB.string_null_check(in reader, 0),
                            DB.int_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4));
                    }
                    );
        }

        public Tuple<QueryStatus, Advisor> get_advisor(string id)
        {
            return Query<Advisor>.lookup_query("SELECT Id, FirstName, LastName, LoginName, Level, Type FROM ADVISOR WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new Advisor(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5))
                            );
                    }
                    );
        }

        public Tuple<QueryStatus, List<Advisor>> get_advisors()
        {
            return SQLiteDB.SQLiteDBAdvisor.get();
        }

        // also add a processing status
        public Tuple<QueryStatus, ProcessingStatus, MeetingAnalytics> get_analytics()
        {
            var (query_status, query_data) = SQLiteDB.SQLiteDBMeetings.get_meetings_asc_by_date();
            if (query_status == QueryStatus.SUCCESS)
            {
                var (analytics_status, analytics_data) = MeetingsProcessing.process_statistics_acs_date_order(query_data);
                return Tuple.Create(query_status, analytics_status, analytics_data);
            }
            return Tuple.Create(query_status, ProcessingStatus.NO_PROCESSING, new MeetingAnalytics());
        }
        public Tuple<QueryStatus, Calendar> get_calendar(string id)
        {
            return SQLiteDB.SQLiteDBCalendar.get(id);
        }

        public Tuple<QueryStatus, List<Calendar>> get_calendars()
        {
            return Query<Calendar>.read_query("SELECT ID, Name, EventCount FROM CALENDAR;", 
                reader =>
                {
                    return (new Calendar(
                        DB.string_null_check(in reader, 0),
                        DB.string_null_check(in reader, 1),
                        DB.int_null_check(in reader, 2)));
                }
                );
        }


        public Tuple<QueryStatus, Founder> get_founder(string id)
        {
            return Query<Founder>.lookup_query("SELECT Id, FirstName, LastName, LoginName, Level, Type FROM FOUNDER WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new Founder(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5))
                            );
                    }
                    );
        }

        public Tuple<QueryStatus, List<Founder>> get_founders()
        {
            return Query<Founder>.read_query("SELECT Id, FirstName, LastName, LoginName, Level, Type FROM FOUNDER;",  
                    reader => {
                        return new Founder(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5))
                            );
                    });
        }

        public Tuple<QueryStatus, List<Meeting>> get_latest_meetings()
        {
            return Query<Meeting>.read_query("SELECT MEETING.MeetingId, ADVISOR.Id, ADVISOR.FirstName, ADVISOR.LastName, Meeting.FounderId, MEETING.MeetingDate, MEETING.Status FROM MEETING INNER JOIN ADVISOR ON Meeting.AdvisorId = Advisor.Id ORDER BY MeetingDate DESC LIMIT 3",
                    reader =>
                    {
                        return new Meeting(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4),
                            DB.string_null_check(in reader, 5),
                            DB.string_null_check(in reader, 6)
                        );
                    }
                    );
        }

        public Tuple<QueryStatus, List<Meeting>> get_meetings_asc_by_date()
        {
            return Query<Meeting>.read_query("SELECT MEETING.MeetingId, ADVISOR.Id, ADVISOR.FirstName, ADVISOR.LastName, Meeting.FounderId, MEETING.MeetingDate, MEETING.Status FROM MEETING INNER JOIN ADVISOR ON Meeting.AdvisorId = Advisor.Id ORDER BY MeetingDate ASC",
                    reader =>
                    {
                        return new Meeting(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4),
                            DB.string_null_check(in reader, 5),
                            DB.string_null_check(in reader, 6)
                        );
                    }
                    );
        }

        public Tuple<QueryStatus, User> get_user(string id)
        {
            throw new NotImplementedException();
        }

        public Tuple<QueryStatus, List<User>> get_users()
        {
                List<User> list = new List<User>();
                Query<User>.read_query(
                    "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM USER",
                    reader =>
                    {
                        User usr = new User(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5))
                            );
                        var contact_number_tuple = SQLiteDBContactNumber.get(usr.id);
                        if (contact_number_tuple.Item1 == QueryStatus.SUCCESS)
                            usr.contact_number = contact_number_tuple.Item2;
                        var addresses_tuple = SQLiteDBAddress.get(usr.id);
                        if (addresses_tuple.Item1 == QueryStatus.SUCCESS) 
                            usr.addresses = addresses_tuple.Item2;
                        var user_extensions_tuple = SQLiteDBUserExtension.get(usr.id);
                        if (user_extensions_tuple.Item1 == QueryStatus.SUCCESS)
                            usr.extensions = user_extensions_tuple.Item2;
                        return usr;
                    },
                    ref list
                    );
                Query<User>.read_query(
                    "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM ADVISOR",
                    reader =>
                    {
                         User usr = new User(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5))
                            );
                        var contact_number_tuple = SQLiteDBContactNumber.get(usr.id);
                        if (contact_number_tuple.Item1 == QueryStatus.SUCCESS)
                            usr.contact_number = contact_number_tuple.Item2;
                        var addresses_tuple = SQLiteDBAddress.get(usr.id);
                        if (addresses_tuple.Item1 == QueryStatus.SUCCESS) 
                            usr.addresses = addresses_tuple.Item2;
                        var user_extensions_tuple = SQLiteDBUserExtension.get(usr.id);
                        if (user_extensions_tuple.Item1 == QueryStatus.SUCCESS)
                            usr.extensions = user_extensions_tuple.Item2;
                        return usr;                       
                    },
                    ref list
                    );
                Query<User>.read_query(
                    "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM FOUNDER",
                    reader =>
                    {
                          User usr = new User(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5))
                            );
                        var contact_number_tuple = SQLiteDBContactNumber.get(usr.id);
                        if (contact_number_tuple.Item1 == QueryStatus.SUCCESS)
                            usr.contact_number = contact_number_tuple.Item2;
                        var addresses_tuple = SQLiteDBAddress.get(usr.id);
                        if (addresses_tuple.Item1 == QueryStatus.SUCCESS) 
                            usr.addresses = addresses_tuple.Item2;
                        var user_extensions_tuple = SQLiteDBUserExtension.get(usr.id);
                        if (user_extensions_tuple.Item1 == QueryStatus.SUCCESS)
                            usr.extensions = user_extensions_tuple.Item2;
                        return usr;                      
                    },
                    ref list
                    );
                return Tuple.Create(QueryStatus.SUCCESS, list);


            throw new NotImplementedException();
        }

        public QueryStatus post_address(Address address)
        {
            throw new NotImplementedException();
        }

        public QueryStatus registrer_user(UserDTOPost dto)
        {
            throw new NotImplementedException();
        }

        public Tuple<QueryStatus, List<Event>> get_events_for_calendar(string id)
        {
            return Query<Event>.read_query(
                    "SELECT(EventId, CalendarId, EventDate, StartTime, EndTime, Title, CreatedAt, UpdatedAt, Status) " +
                    "FROM EVENT WHERE CalendarId = @CalendarId;",
                    p =>
                    {
                        p.AddWithValue("@CalendarId", id);
                    },
                    reader =>
                    {
                        return new Event(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4),
                            DB.string_null_check(in reader, 5),
                            DB.int_null_check(in reader, 6),
                            DB.int_null_check(in reader, 7),
                            DB.string_null_check(in reader, 8)
                            );
                    });
        }

    }

        // the things about the table and etc. that is for implementation, so that code should be moved
        // maybe for the handler or something
        // why even have this dataaccess?
        //return SQLiteDB.SQLiteDBUser.post(dto);

}
*/
