using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Services;

namespace MusicStore.Api.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController(ISaleService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SaleResponseDto>> Get(int id)
    {
        var result = await service.GetAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(
        [FromQuery] string email,
        [FromBody] SaleRequestDto request)
    {
        var id = await service.AddAsync(email, request);
        return Ok(id);
    }

    [HttpGet("by-date")]
    public async Task<ActionResult<ICollection<SaleResponseDto>>> GetByDate(
        [FromQuery] DateTime? start,
        [FromQuery] DateTime? end)
    {
        var result = await service.GetByDateAsync(start, end);
        return Ok(result);
    }

    [HttpGet("by-customer")]
    public async Task<ActionResult<ICollection<SaleResponseDto>>> GetByCustomer(
        [FromQuery] string email,
        [FromQuery] string? title)
    {
        var result = await service.GetByCustomerAsync(email, title);
        return Ok(result);
    }

    [HttpGet("report")]
    public async Task<ActionResult<ICollection<SaleReportResponseDto>>> GetReport(
        [FromQuery] DateTime? start,
        [FromQuery] DateTime? end)
    {
        var result = await service.GetSaleReportAsync(start, end);
        return Ok(result);
    }
}