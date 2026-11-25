using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories;
using MusicStore.Services.Exceptions;

namespace MusicStore.Services;


// primary constructor lo nuevo
public class ConcertService(
    IConcertRepository repository,
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork,
    ILogger<ConcertService> logger, //
    IMapper mapper) : IConcertService
{
    public async Task<ICollection<ConcertResponseDto>> GetAsync(string? title)
    {
        //ESTO LO COMENTAMOS PARA PROBAR LOS METODOS QUE COLOCAMOS EN SQL EN EL REPOSITORIO DE CONCERT
        /*var query = repository
           .Query()
           .Where(x => x.Status)
           .Include(x => x.Genre)
           .AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(x => x.Title.Contains(title));
        }
        
        var entities = await query.ToListAsync();
        */

        //var data = await repository.GetDetailAsync(title); // ESTE ES EL PRIMER METODO PARA PROBAR

        var data = await repository.GetDetailRawAsync(title); // ESTE ES EL 2DO METODO PARA PROBAR
        return mapper.Map<ICollection<ConcertResponseDto>>(data);
    }

    public async Task<ConcertResponseDto> GetAsync(int id)
    {
        var entity = await repository
            .Query()
            .Include(x => x.Genre)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null || !entity.Status)
        {
            logger.LogWarning("Concert {Id} no encontrado", id);
            throw new NotFoundException($"No se encontró el concierto con id {id}");
        }

        return mapper.Map<ConcertResponseDto>(entity);
    }

    public async Task<ConcertResponseDto> AddAsync(ConcertRequestDto request)
    {
        var genre = await genreRepository.GetByIdAsync(request.GenreId);

        if (genre is null)
        {
            logger.LogWarning("Intento de creación de Concert con Genre inexistente {GenreId}", request.GenreId);
            throw new NotFoundException($"No se encontró el género con id {request.GenreId}");
        }

        var entity = mapper.Map<Concert>(request);

        entity.DateEvent = request.DateEvent.ToDateTime(request.TimeEvent);
        entity.Finalized = false;
        entity.Status = true;
        entity.Genre = genre;
        await repository.AddAsync(entity);
        
        await unitOfWork.CommitAsync();

        return mapper.Map<ConcertResponseDto>(entity);
    }

    public async Task<ConcertResponseDto> UpdateAsync(int id, ConcertRequestDto request)
    {
        var genre = await genreRepository.GetByIdAsync(request.GenreId);

        if (genre is null)
        {
            logger.LogWarning("Intento de creación de Concert con Genre inexistente {GenreId}", request.GenreId);
            throw new NotFoundException($"No se encontró el género con id {request.GenreId}");
        }

        var entity = await repository.GetByIdAsync(id);

        if (entity is null || !entity.Status)
        {
            logger.LogWarning("Intento de actualización de Concert inexistente {Id}", id);
            throw new NotFoundException($"No se encontró el concierto con id {id}");
        }

        mapper.Map(request, entity);

        entity.DateEvent = request.DateEvent.ToDateTime(request.TimeEvent);
        entity.Genre = genre;

        await repository.UpdateAsync(entity);
        
        await unitOfWork.CommitAsync();
        
        return mapper.Map<ConcertResponseDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity is null || !entity.Status)
        {
            logger.LogWarning("Intento de eliminación de Concert inexistente {Id}", id);
            throw new NotFoundException($"No se encontró el concierto con id {id}");
        }

        await repository.DeleteAsync(id);
        
        await unitOfWork.CommitAsync();
    }

    public async Task FinalizeAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity is null || !entity.Status)
        {
            logger.LogWarning("Intento de finalización de Concert inexistente {Id}", id);
            throw new NotFoundException($"No se encontró el concierto con id {id}");
        }

        if (entity.Finalized)
        {
            logger.LogWarning("Concert {Id} ya estaba finalizado", id);
            return;
        }

        entity.Finalized = true;

        await repository.UpdateAsync(entity);
        
        await unitOfWork.CommitAsync();
    }
}

