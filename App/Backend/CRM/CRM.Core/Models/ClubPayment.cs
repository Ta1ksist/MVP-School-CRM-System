namespace CRM.Core.Models;

public class ClubPayment
{
    public Guid Id { get; set; }
    public Guid EnrollmentId { get; set; }
    public ClubEnrollment Enrollment { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    
    public ClubPayment(Guid id, Guid enrollmentId, ClubEnrollment enrollment, DateTime paymentDate, decimal amount,
        string paymentMethod)
    {
        Id = id;
        EnrollmentId = enrollmentId;
        Enrollment = enrollment;
        PaymentDate = paymentDate;
        Amount = amount;
        PaymentMethod = paymentMethod;
    }

    public static (ClubPayment clubPayment, string Error) Create(Guid id, Guid enrollmentId, ClubEnrollment enrollment,
        DateTime paymentDate, decimal amount, string paymentMethod)
    {
        var error = "";

        var clubPayment = new ClubPayment(id, enrollmentId, enrollment, paymentDate, amount, paymentMethod);
        
        return (clubPayment, error);
    }
}