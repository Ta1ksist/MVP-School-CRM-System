using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.DTOs;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class ClubEnrollmentService : IClubEnrollmentService
{
    private readonly IClubEnrollmentRepository  _clubEnrollmentRepository;

    public ClubEnrollmentService(IClubEnrollmentRepository clubEnrollmentRepository)
    {
        _clubEnrollmentRepository = clubEnrollmentRepository;
    }

    public async Task<ClubEnrollment> GetClubEnrollmentById(Guid id)
    {
        return await _clubEnrollmentRepository.GetClubEnrollmentById(id);
    }

    public async Task<List<ClubEnrollment>> GetClubEnrollmentByPupilId(Guid pupilId)
    {
        return await _clubEnrollmentRepository.GetClubEnrollmentByPupilId(pupilId);
    }

    public async Task<PupilDebDTO> GetPupilDebt(Guid pupilId)
    {
        var enrollments = await _clubEnrollmentRepository.GetClubEnrollmentByPupilId(pupilId);
        var activeEnrollments = enrollments.Where(e => e.IsActive).ToList();

        decimal totalExpected = 0;
        decimal totalPaid = 0;

        foreach (var enrollment in activeEnrollments)
        {
            var club = enrollment.Club;
            if (club == null) 
                throw new Exception("Club is null in enrollment. Ensure it is loaded.");

            int monthsEnrolled = (int)Math.Ceiling((DateTime.UtcNow - enrollment.EnrollmentDate).TotalDays / 30);
            if (monthsEnrolled < 1) monthsEnrolled = 1;

            totalExpected += monthsEnrolled * club.MonthlyFee;
            totalPaid += enrollment.Payments?.Sum(p => p.Amount) ?? 0;
        }

        return new PupilDebDTO
        {
            PupilId = pupilId,
            TotalExpectedAmount = totalExpected,
            TotalPaidAmount = totalPaid
        };
    }
    
    public async Task<Guid> AddClubEnrollment(ClubEnrollment enrollment)
    {
        return await _clubEnrollmentRepository.AddClubEnrollment(enrollment);
    }

    public async Task<Guid> UpdateClubEnrollment(ClubEnrollment enrollment)
    {
        return await _clubEnrollmentRepository.UpdateClubEnrollment(enrollment);
    }
}