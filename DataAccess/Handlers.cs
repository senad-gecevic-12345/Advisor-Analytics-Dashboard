using MediatR;
using fullstack_1.Data;
using fullstack_1.Requests;
using fullstack_1.Status;
using fullstack_1.Database;


// TODO: remove calls to other queries from that, or just copy paste, and use handler to call other handler when needed
namespace fullstack_1.DataAccess
{

    namespace PersonIdHandler
    {
        using fullstack_1.Requests.PersonIdRequest;
        public class GetPersonIdHandler : IRequestHandler<GetPersonIdRequest, Tuple<QueryStatus, PersonId>>
        {
            public Task<Tuple<QueryStatus, PersonId>> Handle(GetPersonIdRequest request, CancellationToken cancellationToken)
            {
                 throw new NotImplementedException();
            }
        }
        public class CreatePersonIdHandler : IRequestHandler<CreatePersonIdRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(CreatePersonIdRequest request, CancellationToken cancellationToken)
            {
                 throw new NotImplementedException();
            }

        }
        public class DeletePersonIdHandler : IRequestHandler<DeletePersonIdRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(DeletePersonIdRequest request, CancellationToken cancellationToken)
            {
                 throw new NotImplementedException();
            }
        }
        public class DeletePersonIdAddressesHandler : IRequestHandler<DeletePersonIdAddressesRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(DeletePersonIdAddressesRequest request, CancellationToken cancellationToken)
            {
                 throw new NotImplementedException();
            }
        }
    }

    namespace UserHandler
    {
        using fullstack_1.Requests.UserRequests;
        public class GetUserListHandler : IRequestHandler<GetUserListRequest, Tuple<QueryStatus, List<User>>>
        {
            public Task<Tuple<QueryStatus, List<User>>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBUser.get());
            }
        }
        public class GetUserHandler : IRequestHandler<GetUserRequest, Tuple<QueryStatus, User>>
        {
            
            public Task<Tuple<QueryStatus, User>> Handle(GetUserRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBUser.lookup(request.id));
            }

        }

        /*
        public class PostUserHandler : IRequestHandler<PostUserRequest, QueryStatus>
        {
            // maybe here can process what table and whatnot?
            public QueryStatus Handle(PostUserRequest request, CancellationToken cancellationToken)
            {
                var (status, table, user) = SQLiteDB.SQLiteDBUser.post(request.dto);
                switch (status)
                {
                    case Status.QueryStatus.ERROR:
                        break;
                    case Status.QueryStatus.UNSUPORTED:
                        break;
                    case Status.QueryStatus.INVALID_INPUT:
                        break;
                }
                switch (table)
                {
                    case Table.ADVISOR:
                        break;
                    case Table.FOUNDER:
                        break;
                    case Table.USER:
                        break;
                }
                var result = SQLiteDB.SQLiteDBUser.post(request.dto);
                return Task.FromResult();
            }
        }
*/

        public class GetUserAddressesHandler : IRequestHandler<GetUserAddressesRequest, Tuple<QueryStatus, List<Address>>>
        {
            public Task<Tuple<QueryStatus, List<Address>>> Handle(GetUserAddressesRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAddress.get_by_user_id(request.id));
            }
        }
        public class GetAddressesHandler : IRequestHandler<GetAddressesRequest, Tuple<QueryStatus, List<Address>>>
        {
            public Task<Tuple<QueryStatus, List<Address>>> Handle(GetAddressesRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAddress.get());
            }
        }

        public class PostAddressHandler : IRequestHandler<PostAddressRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(PostAddressRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAddress.post(request.dto));
            }
        }

        public class GetAddressHandler : IRequestHandler<GetAddressRequest, Tuple<QueryStatus, List<Address>>>
        {
            public Task<Tuple<QueryStatus, List<Address>>> Handle(GetAddressRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAddress.get_by_user_id(request.id));
            }
        } 
        public class DeleteAddressHandler : IRequestHandler<DeleteAddressRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBPersonId.delete_user_addresses(request.id));
            }
        }

    }

    namespace FounderHandler
    {
        using fullstack_1.Requests.FounderRequests;
        public class GetFounderListHandler : IRequestHandler<GetFounderListRequest, Tuple<QueryStatus, List<Founder>>>
        {
            public Task<Tuple<QueryStatus, List<Founder>>> Handle(GetFounderListRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBFounder.get());
            }
        }

        public class GetFounderHandler : IRequestHandler<GetFounderRequest, Tuple<QueryStatus, Founder>>
        {
            public Task<Tuple<QueryStatus, Founder>> Handle(GetFounderRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBFounder.lookup(request.id));
            }
        }

        public class DeleteFounderHandler : IRequestHandler<DeleteFounderRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(DeleteFounderRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBFounder.delete(request.id));
            }
        }

        public class GetFounderAddressesHandler : IRequestHandler<GetFounderAddressesRequest, Tuple<QueryStatus, List<Address>>>
        {
            public Task<Tuple<QueryStatus, List<Address>>> Handle(GetFounderAddressesRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBFounder.get_addresses(request.id));
            }
        }
    }

    namespace AdvisorHandler
    {
        using fullstack_1.Requests.AdvisorRequests;
        public class GetAdvisorListHandler : IRequestHandler<GetAdvisorListRequest, Tuple<QueryStatus, List<Advisor>>>
        {
            public Task<Tuple<QueryStatus, List<Advisor>>> Handle(GetAdvisorListRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAdvisor.get());
            }
        }

        public class GetAdvisorHandler : IRequestHandler<GetAdvisorRequest, Tuple<QueryStatus, Advisor>>
        {
            public Task<Tuple<QueryStatus, Advisor>> Handle(GetAdvisorRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAdvisor.lookup(request.id));
            }
        }
        public class DeleteAdvisorHandler : IRequestHandler<DeleteAdvisorRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(DeleteAdvisorRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAdvisor.delete(request.id)); 
            }
        }
        public class GetAdvisorAddressesHandler : IRequestHandler<GetAdvisorAddressesRequest, Tuple<QueryStatus, List<Address>>>
        {
            public Task<Tuple<QueryStatus, List<Address>>> Handle(GetAdvisorAddressesRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBAdvisor.get_addresses(request.id));
            }
        }
    }

    namespace MeetingsHandler
    {
        using fullstack_1.Requests.MeetingsRequests;
        public class GetMeetingsHandler : IRequestHandler<GetMeetingsRequest, Tuple<QueryStatus, List<Meeting>>>
        {
            public Task<Tuple<QueryStatus, List<Meeting>>> Handle(GetMeetingsRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBMeetings.get_latest_meetings_limit_3());
            }
        }
        public class GetMeetingsKPIHandler : IRequestHandler<GetMeetingsKPIRequest, Tuple<QueryStatus, MeetingAnalytics>>
        {
            public Task<Tuple<QueryStatus, MeetingAnalytics>> Handle(GetMeetingsKPIRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(Processing.MeetingsProcessing.process_statistics_acs_date_order(SQLiteDB.SQLiteDBMeetings.get_meetings_asc_by_date().Item2));
            }
        }
        public class GetMeetingsKPIListHandler : IRequestHandler<GetMeetingsKPIRequestList, Tuple<QueryStatus, List<MeetingAnalytics>>>
        {
            public Task<Tuple<QueryStatus, List<MeetingAnalytics>>> Handle(GetMeetingsKPIRequestList request, CancellationToken cancellationToken)
            {
                return Task.FromResult(Processing.MeetingsProcessing.process_statistics_acs_date_order_2(SQLiteDB.SQLiteDBMeetings.get_meetings_asc_by_date().Item2));
            }
        }

    }

    namespace GraphHandler
    {
        using fullstack_1.Requests.GraphRequest;

        public class GetTrafficHandler : IRequestHandler<GetTrafficRequest, Tuple<QueryStatus, List<TrafficData>>>
        {
            public Task<Tuple<QueryStatus, List<TrafficData>>> Handle(GetTrafficRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBGraph.get_traffic());
            }

        }
        public class GetApplicationsHandler : IRequestHandler<GetApplicationsRequest, Tuple<QueryStatus, List<ApplicationsData>>>
        {
            public Task<Tuple<QueryStatus, List<ApplicationsData>>> Handle(GetApplicationsRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBGraph.get_applications());
            }

        }

    }
    namespace CompanyHandler
    {
        using fullstack_1.Requests.CompanyRequest;
        public class GetCompaiesHandler : IRequestHandler<GetCompaniesRequest, Tuple<QueryStatus, List<Company>>>
        {
            public Task<Tuple<QueryStatus, List<Company>>> Handle(GetCompaniesRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBCompany.get_companies());
            }

        }

    }

    namespace CalendarController
    {
        using fullstack_1.Requests.CalendarRequest;
        public class GetCalendarsHandler : IRequestHandler<GetCalendarsRequest, Tuple<QueryStatus, List<Calendar>>>
        {
            public Task<Tuple<QueryStatus, List<Calendar>>> Handle(GetCalendarsRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBCalendar.get_calendars());
            }

        }
        public class PostCalendarHandler : IRequestHandler<PostCalendarRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(PostCalendarRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBCalendar.post(request.calendar));
            }
        }


    }

    namespace AddressHander
    {
        using fullstack_1.Requests.AddressRequest;
        public class GetAddressesHander : IRequestHandler<GetAddressesRequest, Tuple<QueryStatus, List<Address>>>
        {
            public Task<Tuple<QueryStatus, List<Address>>> Handle(GetAddressesRequest request, CancellationToken cancellationToken){
                return Task.FromResult(SQLiteDB.SQLiteDBAddress.get());
            }
        }
        public class DeleteAddressHandler  : IRequestHandler<DeleteAddressRequest, QueryStatus>
        {
            public Task<QueryStatus> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(SQLiteDB.SQLiteDBPersonId.delete_user_addresses(request.user_id));
            }
        }
    }
}



