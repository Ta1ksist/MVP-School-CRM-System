namespace CRM.DataAccess.Entities;

public class GradeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<PupilEntity> Pupils { get; set; }
}