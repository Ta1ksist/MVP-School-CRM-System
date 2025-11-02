namespace CRM.DataAccess.Entities;

public class SubjectEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<TeacherEntity> Teachers { get; set; } = new();
}