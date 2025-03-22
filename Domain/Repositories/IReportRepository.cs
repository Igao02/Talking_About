using Talking_About.Domain.Entities;

namespace Talking_About.Domain.Repositories;

public interface IReportRepository
{
    Task<Report> AddAsync(Report report);
    Task DeleteAsync(Guid id);
    Task<Report> EditAsync(Report report);
    Task<Report?> GetAsync(Guid id);
    Task<IEnumerable<Report>> GetListAsync();
    Task<IEnumerable<Report>> GetReportsByTypeAsync(string type);
}