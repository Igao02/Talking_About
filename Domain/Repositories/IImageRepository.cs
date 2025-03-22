using Talking_About.Domain.Entities;

namespace Talking_About.Domain.Repositories;

public interface IImageRepository
{
    Task<Image> AddImageAsync(Image image);
    Task DeleteImageAsync(Guid id);
    Task <Image?> GetImageAsync(Guid id);
    Task<IEnumerable<Image>> GetListAsync();
}
