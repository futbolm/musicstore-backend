using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Services;
using System.Security.Claims;

namespace MusicStore.Api.Controllers;

[ApiController]
[Route("api/sales")]
[Authorize]
public class SalesController(ISaleService service) : ControllerBase
{
    private string GetUserEmail() => User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    private bool IsAdmin() => User.IsInRole("Administrator");


    [HttpGet("{id:int}")]
    public async Task<ActionResult<SaleResponseDto>> Get(int id)
    {
        var result = await service.GetAsync(id);
        
        if (!IsAdmin() && result.Email != GetUserEmail())
        {
            return Forbid();
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(
       // [FromQuery] string email,
        [FromBody] SaleRequestDto request)
    {
        var email = GetUserEmail();
        var id = await service.AddAsync(email, request);
        return Ok(id);
    }

    [HttpGet("by-date")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ICollection<SaleResponseDto>>> GetByDate(
        [FromQuery] DateTime? start,
        [FromQuery] DateTime? end)
    {
        var result = await service.GetByDateAsync(start, end);
        return Ok(result);
    }

    [HttpGet("by-customer")]
    public async Task<ActionResult<ICollection<SaleResponseDto>>> GetByCustomer(
       // [FromQuery] string email,
        [FromQuery] string? title)
    {
        var email = GetUserEmail();
        var result = await service.GetByCustomerAsync(email, title);
        return Ok(result);
    }

    [HttpGet("report")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ICollection<SaleReportResponseDto>>> GetReport(
        [FromQuery] DateTime? start,
        [FromQuery] DateTime? end)
    {
        var result = await service.GetSaleReportAsync(start, end);
        return Ok(result);
    }
}