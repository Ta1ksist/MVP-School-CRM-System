namespace CRM.DataAccess.Entities;

public class PupilEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Guid GradeId { get; set; } 
    public GradeEntity Grade { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public List<ParentEntity> Parents { get; set; } = new();
}