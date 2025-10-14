namespace CRM.DataAccess.Entities;

public class DirectorateEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string PhotoPath { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
}