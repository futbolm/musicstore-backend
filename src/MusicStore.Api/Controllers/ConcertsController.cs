using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Services;

namespace MusicStore.Api.Controllers;

[ApiController]
[Route("api/concerts")]
public class ConcertsController(IConcertService _service) : ControllerBase
{
    // GET api/concerts?title=rock
    [HttpGet]
    public async Task<ActionResult<ICollection<ConcertResponseDto>>> Get(
        [FromQuery] string? title)
    {
        var result = await _service.GetAsync(title);
        return Ok(result);
    }

    // GET api/concerts/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ConcertResponseDto>> Get(int id)
    {
        var result = await _service.GetAsync(id);
        return Ok(result);
    }

    // POST api/concerts
    [HttpPost]
    [Authorize(Roles ="Administrator")]
    public async Task<ActionResult<ConcertResponseDto>> Post(
        [FromBody] ConcertRequestDto request)
    {
        var created = await _service.AddAsync(request);

        return CreatedAtAction(
            nameof(Get),
            new { id = created.Id },
            created);
    }

    // PUT api/concerts/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ConcertResponseDto>> Put(
        int id,
        [FromBody] ConcertRequestDto request)
    {
        var updated = await _service.UpdateAsync(id, request);
        return Ok(updated);
    }

    // DELETE api/concerts/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    // PATCH api/concerts/5/finalize
    [HttpPatch("{id:int}/finalize")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Finalize(int id)
    {
        await _service.FinalizeAsync(id);
        return NoContent();
    }
}