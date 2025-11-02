namespace CRM.DataAccess.Entities;

public class GradeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<PupilEntity> Pupils { get; set; } = new();
}