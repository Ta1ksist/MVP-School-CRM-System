using CRM.Core.Models;

namespace CRM.Core.Abstractions.Repositories;

public interface IClubEnrollmentRepository
{
    Task<ClubEnrollment> GetClubEnrollmentById(Guid id);
    Task<List<ClubEnrollment>> GetClubEnrollmentByPupilId(Guid pupilId);
    Task<List<ClubEnrollment>> GetAllWithPayments();
    Task<Guid> AddClubEnrollment(ClubEnrollment enrollment);
    Task<Guid> UpdateClubEnrollment(ClubEnrollment enrollment);
}