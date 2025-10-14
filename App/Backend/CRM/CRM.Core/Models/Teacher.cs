namespace CRM.Core.Models;

public class Teacher
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string PhotoPath { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public ICollection<Subject> Subjects { get; set; }
    
    public User User { get; set; }
    public Guid UserId { get; set; }
    
    public string FullName => $"{LastName} {FirstName} {Patronymic}";

    public Teacher(Guid id, string firstName, string lastName, string patronymic, DateOnly dateOfBirth,
        string photoPath, string phoneNumber, string email, string address, ICollection<Subject> subjects, User user, Guid userId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        DateOfBirth = dateOfBirth;
        PhotoPath = photoPath;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Subjects = subjects;
        User = user;
        UserId = userId;
    }

    public static (Teacher teacher, string Error) Create(Guid id, string firstName, string lastName,
        string patronymic, string photoPath, DateOnly dateOfBirth, string phoneNumber, string email,
        string address, ICollection<Subject> subjects, User user, Guid userId)
    {
        string error = "";

        if (string.IsNullOrEmpty(firstName)) error = "Строка имя не может быть пустым";
        if (string.IsNullOrEmpty(lastName)) error = "Строка фамилия не может быть пустым";
        if (DateOnly.FromDateTime(DateTime.Now) == dateOfBirth) error = "Дата рождения указана неверно";
        if (string.IsNullOrEmpty(photoPath)) error = "Строка путь к файлу с фото не может быть пустой";
        if (string.IsNullOrEmpty(phoneNumber)) error = "Строка номер телефона не может быть пустой";
        if (string.IsNullOrEmpty(email)) error = "Строка почты не может быть пустой";
        if (string.IsNullOrEmpty(address)) error = "Строка адреса не может быть пустой";

        var teacher = new Teacher(id, firstName, lastName, patronymic, dateOfBirth, photoPath, phoneNumber, email,
            address, subjects, user, userId);

        return (teacher, error);
    }
}