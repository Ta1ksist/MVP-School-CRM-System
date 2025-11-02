namespace CRM.Core.Models;

public class Subject
{
    public Guid Id { get; }
    public string Name { get; set; }
    
    private readonly List<Teacher> _teachers = new();
    public IReadOnlyCollection<Teacher> Teachers => _teachers.AsReadOnly();
    
    public Subject(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static (Subject subject, string Error) Create(Guid id, string name)
    {
        string error = "";

        if (string.IsNullOrEmpty(name)) error = "Строка название предмета не может быть пустой";
        
        var subject = new Subject(id, name);
        
        return (subject, error);
    }
    
    public void AddTeacher(Teacher teacher)
    {
        if (teacher == null) throw new ArgumentNullException(nameof(teacher));
        if (_teachers.Any(p => p.Id == teacher.Id)) return;
        _teachers.Add(teacher);
    }

    public void RemoveTeacher(Guid teacherId)
    {
        var t = _teachers.FirstOrDefault(x => x.Id == teacherId);
        if (t != null) _teachers.Remove(t);
    }
}