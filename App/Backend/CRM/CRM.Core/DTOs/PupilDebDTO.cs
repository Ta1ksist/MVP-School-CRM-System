namespace CRM.Core.DTOs;

public class PupilDebDTO
{
    public Guid PupilId { get; set; }
    public string PupilFirstName { get; set; }
    public string PupilLastName { get; set; }
    public string PupilGrade { get; set; }
    public decimal TotalExpectedAmount { get; set; }
    public decimal TotalPaidAmount { get; set; }
    public decimal Debt => TotalExpectedAmount - TotalPaidAmount;
}