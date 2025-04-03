using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using price.list.Models.PriceList.Respomse;
using price_list.Application.Queries;

namespace price_list.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private readonly IMediator _mediator;   
        public PriceListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<GetResponse>> Get()
        {
            var query = new GetPriceListDataQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
