using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface INewsService
{
    Task<News> GetNewsById(Guid id);
    Task<News> GetNewsByTitle(string title);
    Task<List<News>> GetAllNews();
    Task<Guid> AddNews(News news);
    Task<Guid> UpdateNews(Guid id, string title, string description, DateTime date, string photoPath);
    Task<Guid> DeleteNews(Guid id);
}