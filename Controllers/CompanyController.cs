using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using fullstack_1.Data;

// OK mediator

namespace fullstack_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class companyController : ControllerBase
    {
        private readonly IMediator mediator;
        public companyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet()]
        [Route("/Companies")]
        public async Task<ActionResult<IEnumerable<Company>>> get_companies()
        {
            var result = await mediator.Send(new Requests.CompanyRequest.GetCompaniesRequest());
            return Ok(result.Item2);
        }

    }
}
