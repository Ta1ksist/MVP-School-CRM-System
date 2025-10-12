namespace CRM.Core.Models;

public class Directorate
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string PhotoPath { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public Directorate(Guid id, string firstName, string lastName, string patronymic, DateOnly dateOfBirth,
        string photoPath, string role, string phoneNumber, string email, string address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        DateOfBirth = dateOfBirth;
        PhotoPath = photoPath;
        Role = role;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
    }

    public static (Directorate directorate, string Error) Create(Guid id, string firstName, string lastName,string patronymic,
        DateOnly dateOfBirth, string photoPath, string role, string phoneNumber, string email, string address)
    {
        string error = "";
        
        if (string.IsNullOrEmpty(firstName)) error = "Строка имя не может быть пустым";
        if (string.IsNullOrEmpty(lastName)) error = "Строка фамилия не может быть пустым";
        if (DateOnly.FromDateTime(DateTime.Now) == dateOfBirth) error = "Дата рождения указана неверно";
        if (string.IsNullOrEmpty(role)) error = "Строка должность не может быть пустой";
        if (string.IsNullOrEmpty(photoPath)) error = "Строка путь к файлу с фото не может быть пустой";
        if (string.IsNullOrEmpty(phoneNumber)) error = "Строка номер телефона не может быть пустой";
        if (string.IsNullOrEmpty(email)) error = "Строка почты не может быть пустой";
        if (string.IsNullOrEmpty(address)) error = "Строка адреса не может быть пустой";

        var directorate = new Directorate(id, firstName, lastName, patronymic, dateOfBirth, photoPath, role, phoneNumber, email,
            address);
        
        return (directorate, error);
    }
}