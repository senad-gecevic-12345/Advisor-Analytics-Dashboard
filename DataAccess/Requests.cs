using MediatR;
using fullstack_1.Data;
using fullstack_1.Status;

namespace fullstack_1.Requests
{

    namespace PersonIdRequest
    {

        public record GetPersonIdRequest(string id) : IRequest<Tuple<QueryStatus, PersonId>>;
        // should pass the thing or something. or do check validity
        public record CreatePersonIdRequest(string id, string table) : IRequest<QueryStatus>;
        public record DeletePersonIdRequest(string id) : IRequest<QueryStatus>;
        public record DeletePersonIdAddressesRequest(string id) : IRequest<QueryStatus>;
    }

    namespace UserRequests
    {
        public record GetUserListRequest : IRequest<Tuple<QueryStatus, List<User>>>;
        public record GetUserRequest(string id) : IRequest<Tuple<QueryStatus, User>>;
        // also add url. 
        public record PostUserRequest(UserDTOPost dto) : IRequest<QueryStatus>;
        public record GetUserAddressesRequest(string id) : IRequest<Tuple<QueryStatus, List<Address>>>;

        public record GetAddressesRequest() : IRequest<Tuple<QueryStatus, List<Address>>>;
        public record PostAddressRequest(AddressDTOPOST dto) : IRequest<QueryStatus>;

        // all addresses for founder
        public record GetAddressRequest(string id) : IRequest<Tuple<QueryStatus, List<Address>>>;
        // deletes all addresses to founder
        public record DeleteAddressRequest(string id) : IRequest<QueryStatus>;

    }

    namespace FounderRequests{
        public record GetFounderListRequest : IRequest<Tuple<QueryStatus, List<Founder>>>;
        public record GetFounderRequest(string id) : IRequest<Tuple<QueryStatus, Founder>>;
        public record DeleteFounderRequest(string id) : IRequest<QueryStatus>;
        public record GetFounderAddressesRequest(string id) : IRequest<Tuple<QueryStatus, List<Address>>>;

    } 
    namespace AdvisorRequests{
        

        public record GetAdvisorListRequest : IRequest<Tuple<QueryStatus, List<Advisor>>>;
        public record GetAdvisorRequest(string id) : IRequest<Tuple<QueryStatus, Advisor>>;
        public record DeleteAdvisorRequest(string id) : IRequest<QueryStatus>;
        public record GetAdvisorAddressesRequest(string id) : IRequest<Tuple<QueryStatus, List<Address>>>;

    }
    namespace MeetingsRequests{
        public record GetMeetingsRequest : IRequest<Tuple<QueryStatus, List<Meeting>>>;
        public record GetMeetingsKPIRequest : IRequest<Tuple<QueryStatus, MeetingAnalytics>>;
        public record GetMeetingsKPIRequestList : IRequest<Tuple<QueryStatus, List<MeetingAnalytics>>>;

    }
    namespace GraphRequest
    {
        public record GetTrafficRequest : IRequest<Tuple<QueryStatus, List<TrafficData>>>;
        public record GetApplicationsRequest : IRequest<Tuple<QueryStatus, List<ApplicationsData>>>;

    }
    namespace CompanyRequest
    {
        public record GetCompaniesRequest : IRequest<Tuple<QueryStatus, List<Company>>>;
    }

    namespace CalendarRequest
    {
        public record GetCalendarsRequest : IRequest<Tuple<QueryStatus, List<Calendar>>>;
        public record PostCalendarRequest(Calendar calendar) : IRequest<QueryStatus>;
    }

    namespace AddressRequest
    {
        public record GetAddressesRequest : IRequest<Tuple<QueryStatus, List<Address>>>;
        public record DeleteAddressRequest(string user_id) : IRequest<QueryStatus>;

    }

}
