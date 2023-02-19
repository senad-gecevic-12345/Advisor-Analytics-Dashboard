using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fullstack_1.Data;
using fullstack_1.Database;
using fullstack_1.Status;
using MediatR;

// OK reponse_types

// OK  mediator

namespace fullstack_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class advisorsController :  ControllerBase{

        private readonly IMediator mediator;

        public advisorsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "get_all_advisors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Advisor))]
        public async Task<ActionResult<IEnumerable<Advisor>>> GetAdvisors()
        {
            var(status, list) = await mediator.Send(new Requests.AdvisorRequests.GetAdvisorListRequest());
            return Ok(list);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Advisor))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var (status, advisor) = await mediator.Send(new Requests.AdvisorRequests.GetAdvisorRequest(id));
            return Ok(advisor); 
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Advisor>> delete(string id)
        {
            var status = await mediator.Send(new Requests.AdvisorRequests.DeleteAdvisorRequest(id));

            return Ok(status.ToString());
        }
        [HttpGet("{id}/addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Advisor))]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses(string id)
        {
            var (status, list) = await mediator.Send(new Requests.AdvisorRequests.GetAdvisorAddressesRequest(id));
            if (status == Status.QueryStatus.SUCCESS)
                return base.Ok(list);
            else 
                return base.BadRequest();
        }
    }
}
