using CRM.API.Contracts.Requests;
using CRM.API.Contracts.Responses;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;
    
    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet("ById/{id}")]
    public async Task<ActionResult<News>> GetNewsById(Guid id)
    {
        var news = await _newsService.GetNewsById(id);
        if (news == null) return NotFound();
        var newsResponse = new NewsResponse(
            news.Id,
            news.Title,
            news.Description,
            news.Date,
            news.PhotoPath
            );
        
        return Ok(newsResponse);
    }

    [HttpGet("ById/{title}")]
    public async Task<ActionResult<News>> GetNewsByTitle(string title)
    {
        var news = await _newsService.GetNewsByTitle(title);
        if (news == null) return NotFound();
        var newsResponse = new NewsResponse(
            news.Id,
            news.Title,
            news.Description,
            news.Date,
            news.PhotoPath
            );
        
        return Ok(newsResponse);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<News>>> GetAllNews()
    {
        var news = await _newsService.GetAllNews();
        var newsResposne = news
            .Select(n => new NewsResponse(n.Id, n.Title, n.Description, n.Date, n.PhotoPath));
        
        return Ok(newsResposne);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddNews([FromBody] NewsRequest request)
    {
        var news = new News(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Date,
            request.PhotoPath
            );

        await _newsService.AddNews(news);
        return Ok(news.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateNews(Guid id, string title, string description, DateTime date, string photoPath)
    {
        await _newsService.UpdateNews(id, title, description, date, photoPath);
        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteNews(Guid id)
    {
        await _newsService.DeleteNews(id);
        return Ok(id);
    }
}