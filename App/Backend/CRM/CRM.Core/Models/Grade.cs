using Microsoft.VisualBasic;

namespace CRM.Core.Models;

public class Grade
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Pupil> Pupils { get; set; }

    public Grade(Guid id, string name, ICollection<Pupil> pupils)
    {
        Id = id;
        Name = name;
        Pupils = pupils;
    }

    public static (Grade grade, string Error) Create(Guid id, string name, ICollection<Pupil> pupils)
    {
        string error = "";

        if (string.IsNullOrEmpty(name)) error = "Строка названия класса не может быть пустой";
        if (pupils.Count() == 0) error = "В классе должны быть ученики";
        
        var  grade = new Grade(id, name, pupils);
        return (grade, error);
    }
}