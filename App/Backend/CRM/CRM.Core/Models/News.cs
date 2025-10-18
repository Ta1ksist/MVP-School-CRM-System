namespace CRM.Core.Models;

public class News
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string PhotoPath { get; set; }

    public News(Guid id, string title, string description, DateTime date, string photoPath)
    {
        Id = id;
        Title = title;
        Description = description;
        Date = date;
        PhotoPath = photoPath;
    }

    public static (News news, string Error) Create(Guid id, string title, string description, DateTime date,
        string photoPath)
    {
        string error = "";

        if (string.IsNullOrEmpty(title)) error = "Заголовок новости не может быть пустым";
        if (string.IsNullOrEmpty(description)) error = "Описание новости не может быть пустым";

        var news = new News(id, title, description, date, photoPath);
        
        return (news, error);
    }
}