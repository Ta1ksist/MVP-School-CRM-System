namespace CRM.DataAccess.Entities;

public class EventEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string PhotoPath { get; set; }
}