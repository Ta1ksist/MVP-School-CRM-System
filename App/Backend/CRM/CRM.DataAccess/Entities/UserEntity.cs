using Microsoft.AspNetCore.Identity;

namespace CRM.DataAccess.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public string Role { get; set; }
    public Guid? TeacherId { get; set; }
    public TeacherEntity? Teacher { get; set; }
    public Guid? DirectorateId { get; set; }
    public DirectorateEntity? Directorate { get; set; }
}