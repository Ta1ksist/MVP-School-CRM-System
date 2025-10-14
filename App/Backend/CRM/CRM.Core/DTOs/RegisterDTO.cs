namespace CRM.Core.DTOs;

public class RegisterDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public Guid? TeacherId { get; set; }
    public Guid? DirectorateId { get; set; }
}