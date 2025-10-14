namespace CRM.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    
    public Guid? TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public Guid? DirectorateId { get; set; }
    public Directorate Directorate { get; set; }
    
    public bool IsAdmin => Role?.ToLower() == "admin";
    public bool IsTeacher => Role?.ToLower() == "teacher";
    public bool IsDirector => Role?.ToLower() == "director";

    public User(Guid id, string userName, string passwordHash, string role, Guid? teacherId, Teacher teacher,
        Guid? directorateId, Directorate directorate)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Role = role;
        TeacherId = teacherId;
        Teacher = teacher;
        DirectorateId = directorateId;
        Directorate = directorate;
    }

    public static (User user, string Error) Create(Guid id, string userName, string passwordHash, string role,
        Guid? teacherId, Teacher teacher, Guid? directorateId, Directorate directorate)
    {
        string error = "";
        
        if (string.IsNullOrWhiteSpace(role)) error = "Роль не может быть пустой";
        
        var user = new User(id, userName, passwordHash, role, teacherId, teacher, directorateId, directorate);
        
        return (user, error);
    }
}