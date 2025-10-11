namespace CRM.Core.Models;

public class Pupil
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Grade Grade { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    public ICollection<Parent> Parents { get; set; }

    public Pupil(Guid id, string firstName, string lastName, string patronymic, DateOnly dateOfBirth,
        Grade grade, string phoneNumber, string email, string address, ICollection<Parent> parents)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        DateOfBirth = dateOfBirth;
        Grade = grade;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Parents = parents;
    }

    public static (Pupil pupil, string Error) Create(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, Grade grade, string phoneNumber, string email, string address, ICollection<Parent> parents)
    {
        string error = "";
        
        if (string.IsNullOrEmpty(firstName)) error = "Строка имя не может быть пустым";
        if (string.IsNullOrEmpty(lastName)) error = "Строка фамилия не может быть пустым";
        if (DateOnly.FromDateTime(DateTime.Now) == dateOfBirth) error = "Дата рождения указана неверно";
        if (string.IsNullOrEmpty(grade.Name)) error = "Строка класс не может быть пустой";
        if (string.IsNullOrEmpty(phoneNumber)) error = "Строка номер телефона не может быть пустой";
        if (string.IsNullOrEmpty(email)) error = "Строка почты не может быть пустой";
        if (string.IsNullOrEmpty(address)) error = "Строка адреса не может быть пустой";

        var pupil = new Pupil(id, firstName, lastName, patronymic, dateOfBirth, grade, phoneNumber, email, address, parents);
        
        return (pupil, error);
    }
}