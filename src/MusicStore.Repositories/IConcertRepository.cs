using MusicStore.Entities;
using MusicStore.Persistence.Projections;

namespace MusicStore.Repositories; 

public interface IConcertRepository : IRepositoryBase<Concert, int>
{
    // metodos nuevos para el repositorio de concert
    Task<ICollection<ConcertDetailView>> GetDetailAsync(string? title);
    Task<ICollection<ConcertDetailView>> GetDetailRawAsync(string? title);

}

