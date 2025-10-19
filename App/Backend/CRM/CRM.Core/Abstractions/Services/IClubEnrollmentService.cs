using CRM.Core.DTOs;
using CRM.Core.Models;

namespace CRM.Core.Abstractions.Services;

public interface IClubEnrollmentService
{
    Task<ClubEnrollment> GetClubEnrollmentById(Guid id);
    Task<List<ClubEnrollment>> GetClubEnrollmentByPupilId(Guid pupilId);
    Task<PupilDebDTO> GetPupilDebt(Guid pupilId);
    Task<List<ClubEnrollment>> GetAllWithPayments();
    Task<Guid> AddClubEnrollment(ClubEnrollment enrollment);
    Task<Guid> UpdateClubEnrollment(ClubEnrollment enrollment);
}