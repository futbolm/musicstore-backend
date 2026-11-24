namespace MusicStore.Dto.Request; 
public record GenreRequestDto
{
    public string Name { get; set; } = default!; 
    public bool Status { get; set; } = true;
}

