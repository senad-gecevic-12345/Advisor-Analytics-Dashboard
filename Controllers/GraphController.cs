using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using fullstack_1.Database;
using fullstack_1.Data;
using MediatR;


namespace fullstack_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class graphController : ControllerBase
    {
        private readonly IMediator mediator;

        public graphController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    
        [HttpGet()]
        [Route("/Traffic")]
        public async Task<ActionResult<IEnumerable<TrafficData>>> get_site_traffic()
        {
            var result = await mediator.Send(new Requests.GraphRequest.GetTrafficRequest());
            return Ok(result.Item2);
        }

        [HttpGet()]
        [Route("/Applications")]
        public async Task<ActionResult<IEnumerable<ApplicationsData>>> get_site_applications()
        {
            var result = await mediator.Send(new Requests.GraphRequest.GetApplicationsRequest());
            return Ok(result.Item2);
        }
    }
}
