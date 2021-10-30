using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DarsAsan.Models;

namespace DarsAsan.Repositories
{
    public interface IVideoRepository
    {
        Video GetVideo(Guid id);
        Task<IEnumerable<Video>> GetAllVideosAsync();
        Task<Video> AddVideoAsync(Video video);
        Task<Video> UpdateVideoAsync(Guid id, Video video);
        Task<Video> DeleteVideoAsync(Guid id);
        Task SaveAsync();
    }
}