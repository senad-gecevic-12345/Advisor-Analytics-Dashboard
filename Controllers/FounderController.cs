using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fullstack_1.Data;
using fullstack_1.Database;
using MediatR;

namespace fullstack_1.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class foundersController : ControllerBase
    {

        private readonly IMediator mediator;

        public foundersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "get_all_founders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Founder))]
        public async Task<ActionResult<IEnumerable<Founder>>> Get()
        {
            var (status, list) = await mediator.Send(new Requests.FounderRequests.GetFounderListRequest());
            return Ok(list);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> delete(string id)
        {
            var status = await mediator.Send(new Requests.FounderRequests.DeleteFounderRequest(id));
            return Ok(status.ToString());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Founder))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Founder>> Get(string id)
        {
            var (status, founder) = await mediator.Send(new Requests.FounderRequests.GetFounderRequest(id));

            return Ok(founder);
        }

        [HttpGet("{id}/addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Founder))]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress(string id)
        {
            var (status, list) = await mediator.Send(new Requests.FounderRequests.GetFounderAddressesRequest(id));
            return Ok(list);
        }
    }
}
