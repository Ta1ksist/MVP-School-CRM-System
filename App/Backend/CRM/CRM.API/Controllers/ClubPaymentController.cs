using CRM.API.Contracts.Requests;
using CRM.API.Contracts.Responses;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ClubPaymentController : ControllerBase
{
    private readonly IClubPaymentService _clubPaymentService;

    public ClubPaymentController(IClubPaymentService clubPaymentService)
    {
        _clubPaymentService = clubPaymentService;
    }
    
    [HttpGet("ByEnrollmentId/{enrollmentId}")]
    public async Task<ActionResult<ClubPayment>> GetPaymentsByEnrollmentId(Guid enrollmentId)
    {
        var clubPayment = await _clubPaymentService.GetPaymentsByEnrollmentId(enrollmentId);
        if (clubPayment == null) return NotFound();
        var clubPaymentResponse = new ClubPaymentResponse(
            clubPayment.Id,
            clubPayment.EnrollmentId,
            clubPayment.Enrollment,
            clubPayment.PaymentDate,
            clubPayment.Amount,
            clubPayment.PaymentMethod
            );
        
        return Ok(clubPaymentResponse);
    }

    [HttpPost]
    public async Task<Guid> AddClubPayment([FromBody] ClubPaymentRequest request)
    {
        var clubPayment = new ClubPayment(
            Guid.NewGuid(),
            request.EnrollmentId,
            request.Enrollment,
            request.PaymentDate,
            request.Amount,
            request.PaymentMethod
            );
        
        await _clubPaymentService.AddClubPayment(clubPayment);
        return clubPayment.Id;
    }
}