using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Split.Application.Commands;
using Split.Application.Queries;

namespace Split.Api.Controllers
{
    [ApiController, Route("[Controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IMediator _mediator;

        public ExpenseController(ILogger<ExpenseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ExpensesList> Get()
        {
            _logger.LogDebug("ExpenseController :: Get expenses");
            var result = await _mediator.Send(new QueryExpenses());
            return result.Data;
        }

        [HttpPost]
        public async Task<ActionResult> Save(SaveExpense request)
        {
            _logger.LogDebug("ExpenseController :: Save expense");
            var result = await _mediator.Send(request);
            return result.Failed ? BadRequest(result.Message) : Ok();
        }
    }
}