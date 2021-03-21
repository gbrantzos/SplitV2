using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Split.Application.Commands;
using Split.Application.Queries;

namespace Split.Api.Controllers
{
    /// <summary>
    /// Expenses controller
    /// </summary>
    [ApiController, Route("[Controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IMediator _mediator;

        public ExpensesController(ILogger<ExpensesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Returns a list of ExpenseItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ExpensesList> Get()
        {
            _logger.LogDebug("ExpenseController :: Get expenses");
            var result = await _mediator.Send(new QueryExpenses {IncludeAll = true});
            return result.Data;
        }

        /// <summary>
        /// Insert or update an expense entry
        /// </summary>
        /// <remarks>
        /// ## Sample request:
        ///
        ///     POST /Expenses
        ///     {
        ///         "id": 0,
        ///         "description": "Sample expense",
        ///         "amount": 32.45,
        ///         "forOwner": false
        ///     }
        ///
        /// When posting with `id=0`, a new Expense is created. When `id` is not 0,
        /// you must also provide `rowVersion` a value for optimistic concurrency. 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Save(SaveExpense request)
        {
            _logger.LogDebug("ExpenseController :: Save expense");
            var result = await _mediator.Send(request);
            return result.Failed ? BadRequest(result.Message) : Ok();
        }
    }
}