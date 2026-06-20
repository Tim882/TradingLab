using Microsoft.AspNetCore.Mvc;
using TradingLab.Journal.Application.DTOs;
using TradingLab.Journal.Application.Interfaces;

namespace TradingLab.Journal.Controllers
{
    [Route("api/[controller]")]
    public class PositionsController : Controller
    {
        private readonly IPositionDataService _dataService;

        public PositionsController(IPositionDataService dataService)
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
        public async Task<IActionResult> PostAsync([FromBody] PositionRequest request)
        {
            await _dataService.CreateAsync(request);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] PositionRequest request)
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
    }
}

