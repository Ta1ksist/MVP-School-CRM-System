namespace CRM.DataAccess.Entities;

public class ClubPaymentEntity
{
    public Guid Id { get; set; }
    public Guid EnrollmentId { get; set; }
    public ClubEnrollmentEntity Enrollment { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
}