namespace CRM.DataAccess.Entities;

public class NewsEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string PhotoPath { get; set; }
}