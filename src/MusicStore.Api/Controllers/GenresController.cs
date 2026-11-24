using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Services;

namespace MusicStore.Api.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _service;

    public GenresController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GenreResponseDto>>> Get()
    {
        var genres = await _service.GetAsync();
        return Ok(genres);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreResponseDto>> GetById(int id)
    {
        var genre = await _service.GetByIdAsync(id);
        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponseDto>> Post(
        [FromBody] GenreRequestDto request)
    {
        var created = await _service.AddAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GenreResponseDto>> Put(
        int id,
        [FromBody] GenreRequestDto request)
    {
        var updated = await _service.UpdateAsync(id, request);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}