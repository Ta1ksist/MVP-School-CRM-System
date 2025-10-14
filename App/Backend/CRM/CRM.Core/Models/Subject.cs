namespace CRM.Core.Models;

public class Subject
{
    public Guid Id { get; }
    public string Name { get; set; }
    
    public Teacher Teachers { get; set; }
    
    public Subject(Guid id, string name, Teacher teachers)
    {
        Id = id;
        Name = name;
        Teachers = teachers;
    }

    public static (Subject subject, string Error) Create(Guid id, string name, Teacher teachers)
    {
        string error = "";

        if (string.IsNullOrEmpty(name)) error = "Строка название предмета не может быть пустой";
        
        var subject = new Subject(id, name, teachers);
        
        return (subject, error);
    }
}