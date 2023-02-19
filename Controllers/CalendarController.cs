using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fullstack_1.Data;
using fullstack_1.Database;
using fullstack_1.Status;
using MediatR;

// OK mediator

namespace fullstack_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calendarsController : ControllerBase
    {
        private readonly IMediator mediator;

        public calendarsController(Mediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "get_all_calendars")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Calendar))]
        public async Task<ActionResult<IEnumerable<Calendar>>> Get()
        {
            var (request, list) = await mediator.Send(new Requests.CalendarRequest.GetCalendarsRequest());

            if (request == Status.QueryStatus.SUCCESS)
                return Ok(list);
            else
                return BadRequest();
        }   


        [HttpPost(Name = "Post")]
        public async Task<ActionResult<string>> Post([FromBody] Calendar calendar)
        {
            var status = await mediator.Send(new Requests.CalendarRequest.PostCalendarRequest(calendar));
            return Ok(status.ToString());
        }
    }
}
