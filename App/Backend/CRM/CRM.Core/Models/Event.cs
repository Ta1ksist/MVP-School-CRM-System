namespace CRM.Core.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string PhotoPath { get; set; }

    public Event(Guid id, string name, string description, DateTime date, string photoPath)
    {
        Id = id;
        Name = name;
        Description = description;
        Date = date;
        PhotoPath = photoPath;
    }

    public static (Event eevent, string Error) Create(Guid id, string name, string description, DateTime date,
        string photoPath)
    {
        string error = "";

        if (string.IsNullOrEmpty(name)) error = "Название мероприятия не может быть пустым";
        if (string.IsNullOrEmpty(description)) error = "Описание мероприятия не может быть пустым";
        
        var eevent = new Event(id, name, description, date, photoPath);
        
        return (eevent, error);
    }
}