using AutoMapper;
using CRM.API.Contracts.Requests;
using CRM.API.Contracts.Responses;
using CRM.Core.Abstractions.Services;
using CRM.Core.DTOs;
using CRM.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

// [Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ClubEnrollmentController : ControllerBase
{
   private readonly IClubEnrollmentService _clubEnrollmentService;
   private readonly IMapper _mapper;

   public ClubEnrollmentController(IClubEnrollmentService clubEnrollmentService, IMapper mapper)
   {
      _clubEnrollmentService = clubEnrollmentService;
      _mapper = mapper;
   }

   [HttpGet("ById/{id}")]
   public async Task<ActionResult<ClubEnrollment>> GetClubEnrollmentById(Guid id)
   {
      var clubEnrollment = await _clubEnrollmentService.GetClubEnrollmentById(id);
      if (clubEnrollment == null) return NotFound();
      var clubEnrollmentResponse = new ClubEnrollmentResponse(
         clubEnrollment.Id,
         clubEnrollment.ClubId,
         clubEnrollment.Club,
         clubEnrollment.PupilId,
         clubEnrollment.EnrollmentDate,
         clubEnrollment.IsActive,
         clubEnrollment.Payments
         );
      
      return Ok(clubEnrollmentResponse);
   }

   [HttpGet("ByPupilId/{pupilId}")]
   public async Task<ActionResult<List<ClubEnrollment>>> GetClubEnrollmentByPupilId(Guid pupilId)
   {
      var clubEnrollment = await _clubEnrollmentService.GetClubEnrollmentByPupilId(pupilId);
      if (clubEnrollment == null) return NotFound();
      var clubEnrollmentResponse = _mapper.Map<List<ClubEnrollmentResponse>>(clubEnrollment);
      
      return Ok(clubEnrollmentResponse);
   }

   [HttpGet("pupilDebt")]
   public async Task<ActionResult<PupilDebDTO>> GetPupilDebt(Guid pupilId)
   {
      var pupilDeb = await _clubEnrollmentService.GetPupilDebt(pupilId);
      if (pupilDeb == null) return NotFound();
      var pupilDebResponse = new PupilDebDTOResponse(
         pupilDeb.PupilId,
         pupilDeb.PupilFirstName,
         pupilDeb.PupilLastName,
         pupilDeb.PupilGrade,
         pupilDeb.TotalExpectedAmount,
         pupilDeb.TotalPaidAmount
         );
      
      return Ok(pupilDebResponse);
   }

   [HttpGet("allWithPayments")]
   public async Task<ActionResult<List<ClubEnrollment>>> GetAllWithPayments()
   {
      var clubEnrollment = await _clubEnrollmentService.GetAllWithPayments();
      if (clubEnrollment == null) return NotFound();
      var clubEnrollmentResponse = _mapper.Map<ClubEnrollmentResponse>(clubEnrollment);
      
      return Ok(clubEnrollmentResponse);
   }

   [HttpPost]
   public async Task<ActionResult<Guid>> AddClubEnrollment([FromBody] ClubEnrollmentRequest request)
   {
      var clubEnrollment = new ClubEnrollment(
         Guid.NewGuid(),
         request.ClubId,
         request.Club,
         request.PupilId,
         request.EnrollmentDate,
         request.IsActive,
         request.Payments
      );

      await _clubEnrollmentService.AddClubEnrollment(clubEnrollment);
      return Ok(clubEnrollment.Id);
   }

   [HttpPut("{id:guid}")]
   public async Task<ActionResult<Guid>> UpdateClubEnrollment(Guid id, Guid clubId, Club club, Guid pupilId, bool isActive)
   {
      await _clubEnrollmentService.UpdateClubEnrollment(id, clubId, club, pupilId, isActive);
      return Ok(id);
   }
}