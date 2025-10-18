namespace CRM.DataAccess.Entities;

public class ClubEnrollmentEntity
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public ClubEntity Club { get; set; }
    public Guid PupilId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ClubPaymentEntity> Payments { get; set; }
}