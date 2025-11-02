using CRM.API.Contracts.Requests;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("debt")]
    public async Task<ActionResult> SendDebtNotification(string toEmail, string pupilFirstName, string pupilLastName, decimal debt)
    {
        await _notificationService.SendDebtNotification(toEmail, pupilFirstName, pupilLastName, debt);
        return Ok();
    }
    
    [HttpPost("event")]
    public async Task<ActionResult> SendEventNotification([FromBody] NotificationRequest notification)
    {
        await _notificationService.SendEventNotification(notification.Emails, notification.Event);
        return Ok();
    }
    
    [HttpPost("eventChange")]
    public async Task<ActionResult> SendAboutEventChangeNotification(IEnumerable<string> emails, string message)
    {
        await _notificationService.SendAboutEventChangeNotification(emails, message);
        return Ok();
    }
    
    [HttpPost("news")]
    public async Task<ActionResult> SendNewsNotification([FromBody] NotificationRequest notification)
    {
        await _notificationService.SendNewsNotification(notification.Emails, notification.News);
        return Ok();
    }
}