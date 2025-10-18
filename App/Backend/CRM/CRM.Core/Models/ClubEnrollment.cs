namespace CRM.Core.Models;

public class ClubEnrollment
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public Club Club { get; set; }
    public Guid PupilId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ClubPayment> Payments { get; set; }

    public ClubEnrollment(Guid id, Guid clubId, Club club, Guid pupilId, DateTime enrollmentDate,
        bool isActive, ICollection<ClubPayment> payments)
    {
        Id = id;
        ClubId = clubId;
        Club = club;
        PupilId = pupilId;
        EnrollmentDate = enrollmentDate;
        IsActive = isActive;
        Payments = payments;
    }

    public static (ClubEnrollment clubEnrollment, string Error) Create(Guid id, Guid clubId, Club club, Guid pupilId,
        DateTime enrollmentDate, bool isActive, ICollection<ClubPayment> payments)
    {
        string error = "";

        var clubEnrollment = new ClubEnrollment(id, clubId, club, pupilId, enrollmentDate, isActive, payments);
        
        return (clubEnrollment, error);
    }
}