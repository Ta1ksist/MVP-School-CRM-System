namespace CRM.Core.Models;

public class Subject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Teacher> Teachers { get; set; }
    
    public Subject(Guid id, string name, ICollection<Teacher> teachers)
    {
        Id = id;
        Name = name;
        Teachers = teachers;
    }

    public static (Subject subject, string Error) Create(Guid id, string name, ICollection<Teacher> teachers)
    {
        string error = "";

        if (string.IsNullOrEmpty(name)) error = "Строка название предмета не может быть пустой";
        
        var subject = new Subject(id, name, teachers);
        
        return (subject, error);
    }
}