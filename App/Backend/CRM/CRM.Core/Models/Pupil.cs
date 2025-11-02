namespace CRM.Core.Models;

public class Pupil
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Guid GradeId { get; set; } 
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    private readonly List<Parent> _parents = new();
    public IReadOnlyCollection<Parent> Parents => _parents.AsReadOnly();
    
    public string FullName => $"{LastName} {FirstName} {Patronymic}";

    public Pupil(Guid id, string firstName, string lastName, string patronymic, DateOnly dateOfBirth,
        Guid gradeId, string phoneNumber, string email, string address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        DateOfBirth = dateOfBirth;
        GradeId = gradeId;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
    }

    public static (Pupil pupil, string Error) Create(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, Guid gradeId,string phoneNumber, string email, string address)
    {
        string error = "";
        
        if (string.IsNullOrEmpty(firstName)) error = "Строка имя не может быть пустым";
        if (string.IsNullOrEmpty(lastName)) error = "Строка фамилия не может быть пустым";
        if (DateOnly.FromDateTime(DateTime.Now) == dateOfBirth) error = "Дата рождения указана неверно";
        if (string.IsNullOrEmpty(phoneNumber)) error = "Строка номер телефона не может быть пустой";
        if (string.IsNullOrEmpty(email)) error = "Строка почты не может быть пустой";
        if (string.IsNullOrEmpty(address)) error = "Строка адреса не может быть пустой";

        var pupil = new Pupil(id, firstName, lastName, patronymic, dateOfBirth, gradeId, phoneNumber, email, address);
        
        return (pupil, error);
    }
    public void SetGrade(Guid gradeId)
    {
        GradeId = gradeId;
    }
    
    public void AddParent(Parent parent)
    {
        if (parent == null) throw new ArgumentNullException(nameof(parent));
        if (_parents.Any(p => p.Id == parent.Id)) return;
        _parents.Add(parent);
    }
    
    public void RemoveParent(Guid parentId)
    {
        var p = _parents.FirstOrDefault(x => x.Id == parentId);
        if (p != null) _parents.Remove(p);
    }
}