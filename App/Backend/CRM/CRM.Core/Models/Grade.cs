using Microsoft.VisualBasic;

namespace CRM.Core.Models;

public class Grade
{
    public Guid Id { get; }
    public string Name { get; set; }
    
    private readonly List<Pupil> _pupils = new();
    public IReadOnlyCollection<Pupil> Pupils => _pupils.AsReadOnly();
    
    public Grade(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static (Grade grade, string Error) Create(Guid id, string name, ICollection<Pupil> pupils)
    {
        string error = "";

        if (string.IsNullOrEmpty(name)) error = "Строка названия класса не может быть пустой";
        if (pupils == null) error = "В классе должны быть ученики";
        
        var  grade = new Grade(id, name);
        return (grade, error);
    }
    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название класса не может быть пустым");
        Name = name;
    }

    public void AddPupil(Pupil pupil)
    {
        if (pupil == null) throw new ArgumentNullException(nameof(pupil));
        if (_pupils.Any(p => p.Id == pupil.Id)) return;
        _pupils.Add(pupil);
    }

    public void RemovePupil(Guid pupilId)
    {
        var p = _pupils.FirstOrDefault(x => x.Id == pupilId);
        if (p != null) _pupils.Remove(p);
    }
}