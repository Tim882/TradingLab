using Microsoft.AspNetCore.Mvc;
using TradingLab.Journal.Application.Common.Pagination;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;

namespace TradingLab.Journal.Controllers
{
    [Route("api/[controller]")]
    public class TradingAccountsController : Controller
    {
        private readonly ITradingAccountDataService _dataService;

        public TradingAccountsController(ITradingAccountDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _dataService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TradingAccountRequest request)
        {
            await _dataService.CreateAsync(request);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] TradingAccountRequest request)
        {
            await _dataService.UpdateAsync(id, request);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _dataService.DeleteAdync(id);

            return NoContent();
        }

        [HttpGet("filter")]
        [ProducesResponseType(typeof(PaginatedList<TradingAccountResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<TradingAccountResponse>>> GetFiltered(
            [FromQuery] TradingAccountFilterDto filter,
            CancellationToken ct)
        {
            var result = await _dataService.GetFilteredAsync(filter, ct);

            return Ok(result);
        }
    }
}

