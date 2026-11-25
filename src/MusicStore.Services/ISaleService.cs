using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Services;
public interface ISaleService
{
    Task<int> AddAsync(string email, SaleRequestDto request);
    Task<SaleResponseDto> GetAsync(int id);
    Task<ICollection<SaleResponseDto>> GetByDateAsync(DateTime? start, DateTime? end);
    Task<ICollection<SaleResponseDto>> GetByCustomerAsync(string email, string? title);
    Task<ICollection<SaleReportResponseDto>> GetSaleReportAsync(DateTime? dateStart, DateTime? dateEnd);
}
