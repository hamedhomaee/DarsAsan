using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarsAsan.Data;
using DarsAsan.Models;
using Microsoft.EntityFrameworkCore;

namespace DarsAsan.Repositories
{
    public class SQLVideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public SQLVideoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Video GetVideo(Guid id)
        {
            return _context.Videos.Single(v => v.Id == id);
        }

        public async Task<IEnumerable<Video>> GetAllVideosAsync()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<Video> AddVideoAsync(Video video)
        {
            await _context.Videos.AddAsync(video);
            return video;
        }

        public async Task<Video> UpdateVideoAsync(Guid id, Video video)
        {
            Video theVideo = await _context.Videos.FirstOrDefaultAsync(v => v.Id == id);

            if (theVideo != null)
            {
                theVideo.Path = video.Path;
                theVideo.Title = video.Title;
            }

            return null;
        }

        public async Task<Video> DeleteVideoAsync(Guid id)
        {
            Video theVideo = await _context.Videos.FirstOrDefaultAsync(v => v.Id == id);

            if (theVideo != null)
            {
                _context.Videos.Remove(theVideo);
            }

            return null;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}