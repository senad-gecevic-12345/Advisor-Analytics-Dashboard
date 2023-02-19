using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fullstack_1.Data;
using fullstack_1.Processing;
using fullstack_1.Database;
using fullstack_1.Requests;

namespace fullstack_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class meetingsController : ControllerBase
    {
        // meetings/latest
        // meetings/analytics
        private readonly IMediator mediator;
        public meetingsController(IMediator mediator)
        {
            this.mediator = mediator;   
        }

        [HttpGet]
        [Route("/meetings")]
        public async Task<ActionResult<IEnumerable<Meeting>>> get()
        {
            var result = await mediator.Send(new Requests.MeetingsRequests.GetMeetingsRequest());
            return Ok(result.Item2);
            //return new List<Meeting>();
        }
        [HttpGet]
        [Route("/meetingsanalytics")]
        public async Task<ActionResult<MeetingAnalytics>> get_analytics()
        {
            var res = await mediator.Send(new Requests.MeetingsRequests.GetMeetingsKPIRequest());
            return Ok(res.Item2);
        }
        [HttpGet]
        [Route("/meetingsanalytics_2")]
        public async Task<ActionResult<IEnumerable<MeetingAnalytics>>> get_analytics_2()
        {
            var res = await mediator.Send(new Requests.MeetingsRequests.GetMeetingsKPIRequestList());
            return Ok(res.Item2);
        }
    }
}

