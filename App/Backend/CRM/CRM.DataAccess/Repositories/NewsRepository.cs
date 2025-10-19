using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRM.DataAccess.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public NewsRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<News> GetNewsById(Guid id)
    {
        var newsEntity = await _context.News
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();
        var news = _mapper.Map<News>(newsEntity);
        return news;
    }
    
    public async Task<News> GetNewsByTitle(string title)
    {
        var newsEntity = await _context.News
            .Where(n => n.Title == title)
            .FirstOrDefaultAsync();
        var news =  _mapper.Map<News>(newsEntity);
        return news;
    }
    
    public async Task<List<News>> GetAllNews()
    {
        var newsEntity = await _context.News
            .AsNoTracking()
            .ToListAsync();
        var news = _mapper.Map<List<News>>(newsEntity);
        return news;
    }

    public async Task<Guid> AddNews(News news)
    {
        var newsEntity = _mapper.Map<NewsEntity>(news);

        await _context.News.AddAsync(newsEntity);
        await _context.SaveChangesAsync();
        return newsEntity.Id;
    }

    public async Task<Guid> UpdateNews(Guid id, string title, string description, DateTime date, string photoPath)
    {
        var newsEntity = await _context.News
            .Where(n => n.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.Title, title)
                .SetProperty(n => n.Description, description)
                .SetProperty(n => n.Date, date)
                .SetProperty(n => n.PhotoPath, photoPath));

        return id;
    }

    public async Task<Guid> DeleteNews(Guid id)
    {
        var newsEntity = await _context.News
            .Where(n => n.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}