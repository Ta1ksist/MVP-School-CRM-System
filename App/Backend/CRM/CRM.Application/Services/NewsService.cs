using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;
    
    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task<News> GetNewsByTitle(string title)
    {
        return await _newsRepository.GetNewsByTitle(title);
    }

    public async Task<List<News>> GetAllNews()
    {
        return await _newsRepository.GetAllNews();
    }

    public async Task<Guid> AddNews(News news)
    {
        return await _newsRepository.AddNews(news);
    }

    public async Task<Guid> UpdateNews(Guid id, string title, string description, DateTime date, string photoPath)
    {
        return await _newsRepository.UpdateNews(id, title, description, date, photoPath);
    }

    public async Task<Guid> DeleteNews(Guid id)
    {
        return await _newsRepository.DeleteNews(id);
    }
}