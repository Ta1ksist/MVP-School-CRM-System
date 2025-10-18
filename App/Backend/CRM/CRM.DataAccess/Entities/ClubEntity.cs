namespace CRM.DataAccess.Entities;

public class ClubEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal MonthlyFee { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ClubEnrollmentEntity> Enrollments { get; set; }
}