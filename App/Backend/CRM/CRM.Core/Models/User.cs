namespace CRM.Core.Models;

public class User
{
    public Guid Id { get; }
    public string UserName { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }
    public Guid? TeacherId { get; private set; }
    public Teacher? Teacher { get; private set; }
    public Guid? DirectorateId { get; private set; }
    public Directorate? Directorate { get; private set; }

    public bool IsAdmin => Role?.ToLower() == "admin";
    public bool IsTeacher => Role?.ToLower() == "teacher";
    public bool IsDirector => Role?.ToLower() == "director";
    

    private User(Guid id, string userName, string passwordHash, string role)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Role = role;
    }
    public User(Guid id, string userName, string passwordHash, string role, Guid? teacherId = null, Teacher? teacher = null,
        Guid? directorateId = null, Directorate? directorate = null)
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
        Guid? teacherId = null, Teacher? teacher = null, Guid? directorateId = null, Directorate? directorate = null)
    {
        string error = "";

        if (string.IsNullOrWhiteSpace(role)) error = "Роль не может быть пустой";

        var user = new User(id, userName, passwordHash, role, teacherId, teacher, directorateId, directorate);
        return (user, error);
    }
    public void AssignTeacher(Guid teacherId, Teacher teacher)
    {
        TeacherId = teacherId;
        Teacher = teacher;
    }

    public void AssignDirectorate(Guid directorateId, Directorate directorate)
    {
        DirectorateId = directorateId;
        Directorate = directorate;
    }

    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void ChangeRole(string newRole)
    {
        Role = newRole;
    }
}