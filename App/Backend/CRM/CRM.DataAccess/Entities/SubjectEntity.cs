namespace CRM.DataAccess.Entities;

public class SubjectEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<TeacherEntity> Teachers { get; set; }
}