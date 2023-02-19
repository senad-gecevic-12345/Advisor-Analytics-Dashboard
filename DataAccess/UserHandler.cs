using MediatR;
using fullstack_1.Data;
using fullstack_1.Status;
namespace fullstack_1.DataAccess
{
    /*
    public class GetUserHandler : IRequestHandler<GetUserRequest, Tuple<SQLiteStatus, User>>
    {

        private readonly IMediator mediator;
        public GetUserHandler(IDataAccess data)
        {

        }
        public Task<Tuple<SQLiteStatus, User>> Handle()
        {
            var tuple = Tuple.Create(SQLiteStatus.ERROR, new User());



            var task = mediator.Send(tuple);
            return Task.FromResult(tuple);
        }
    }
    public class GetUserListHandler : IRequestHandler<GetUserListRequest, Tuple<SQLiteStatus, List<User>>>
    {

    }
    */
}
