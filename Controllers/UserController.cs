using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fullstack_1.Data;
using fullstack_1.Database;
using System.Net;
using fullstack_1.Status;
using fullstack_1.Requests;

// remove the database includes and etc

// no reason to cache. like why cache. like why
// so much data that are not used
namespace fullstack_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {

        private readonly IMediator mediator;


        public usersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpDelete(" includes{id}")]
        public ActionResult<User> delete(string id)
        {

            return new User();
        }
        [HttpGet(Name = "get_all_users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<ActionResult<List<User>>> Get()
        {
            var result = await mediator.Send(new Requests.UserRequests.GetUserListRequest());
            List < User > list  = result.Item2;
            return Ok(result.Item2);


        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<ActionResult<User>> Get(string id)
        {

            var (status, list) = await mediator.Send(new Requests.UserRequests.GetUserRequest(id));

            if(status == Status.QueryStatus.SUCCESS)
                return Ok(list);
            else
                return BadRequest();
        }


        [HttpGet()]
        [Route("addresses/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Founder))]
        public async Task<ActionResult<IEnumerable<Address>>> _GetAddress(string id)
        {
            var (status, list) = await mediator.Send(new Requests.UserRequests.GetUserAddressesRequest(id));
            return Ok(list);
            //return Ok(SQLiteDB.SQLiteDBAddress.get_by_address_id(id));
        }
        [HttpGet("{id}/addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Founder))]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress(string id)
        {
            var (status, list) = await mediator.Send(new Requests.UserRequests.GetUserAddressesRequest(id));
            return Ok(list);

        }
        [HttpGet]
        [Route("/addresses")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var (status, list) = await mediator.Send(new Requests.AddressRequest.GetAddressesRequest());
            return Ok(list);
        }
        [HttpDelete]
        [Route("/addresses/{id}")]
        public async Task<ActionResult<Address>> delete_address_by_id(string id)
        {
            var result = await mediator.Send(new Requests.PersonIdRequest.DeletePersonIdAddressesRequest(id));
            return Ok(result.ToString());
        }

        [HttpPost]
        [Route("/addresses")]
        public async Task<ActionResult<string>> post_address([FromBody] AddressDTOPOST address)
        {
            var result = mediator.Send(new Requests.UserRequests.PostAddressRequest(address));
            return Ok(result.ToString());

        }
        public static bool try_get_url(out string output_url, IUrlHelper url, string controller_name, string method, object value)
        {
            string? _url = UrlHelperExtensions.Action(url, controller_name, method, value);
            if (_url == null)
            {
                output_url = "";
                return false;
            }
            else
            {
                output_url = _url;
                return true;
            }
        }

        [HttpPost]
        [Route("/register")]
        public ActionResult<User> register([FromBody] UserDTOPost dto)
        {
            var (status, table, user) = SQLiteDB.SQLiteDBUser.post(dto);
            if (user == null)
                return StatusCode(500);
            switch (status)
            {
                case Status.QueryStatus.ERROR:
                    return base.StatusCode(500);
                case Status.QueryStatus.UNSUPORTED:
                    return base.StatusCode(500);
                case Status.QueryStatus.INVALID_INPUT:
                    return base.BadRequest();
            }
            string url;
            switch (table)
            {
                case Table.ADVISOR:
                    if (try_get_url(out url, "get", "advisors", user.id)) ;
                    else
                        return StatusCode(500);
                    break;
                case Table.FOUNDER:
                    if (try_get_url(out url, "get", "founders", user.id)) ;
                    else
                        return StatusCode(500);
                    break;
                case Table.USER:
                    if (try_get_url(out url, "get", "users", user.id)) ;
                    else
                        return StatusCode(500);
                    break;
                default:
                    return StatusCode(500);
            }
            return Created(url, user);
        }

        [HttpGet]
        [Route("/login")]
        public User login()
        {
            return new User();
        }
        private bool try_get_url(out string output_url, string controller_name, string method)
        {
            string host = this.Request.Scheme + "://" + this.Request.Host;
            string? url = UrlHelperExtensions.Action(Url, controller_name, method);
            if (url == null)
            {
                output_url = "";
                return false;
            }
            else
            {
                output_url = host + url;
                return true;
            }
        }
        // users without controller. and then method. idk if value works. and need server name
        private bool try_get_url(out string output_url, string method, string controller_name, object value)
        {
            string host = this.Request.Scheme + "://" + this.Request.Host;
            string? url = UrlHelperExtensions.Action(Url, method, controller_name, new {id = value});
            if (url == null)
            {
                output_url = "";
                return false;
            }
            else
            {
                output_url = host + url;
                return true;
            }
        }
        
    }
}
