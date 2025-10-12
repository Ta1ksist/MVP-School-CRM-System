using System.Runtime.InteropServices.JavaScript;

namespace CRM.Core.Models;

public class Parent
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    public Guid PupilId { get; set; }
    public Pupil Pupil { get; set; }

    public Parent(Guid id, string firstName, string lastName, string patronymic, DateOnly dateOfBirth,
        string role, string phoneNumber, string email, string address, Guid pupilId,Pupil pupil)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        DateOfBirth = dateOfBirth;
        Role = role;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        PupilId = pupilId;
        Pupil = pupil;
    }

    public static (Parent parent, string Error) Create(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, string role, string phoneNumber, string email, string address, Guid pupilId,Pupil pupil)
    {
        string error = "";
        
        if (string.IsNullOrEmpty(firstName)) error = "Строка имя не может быть пустым";
        if (string.IsNullOrEmpty(lastName)) error = "Строка фамилия не может быть пустым";
        if (DateOnly.FromDateTime(DateTime.Now) == dateOfBirth) error = "Дата рождения указана неверно";
        if (string.IsNullOrEmpty(role)) error = "Строка роли не может быть пустой";
        if (string.IsNullOrEmpty(phoneNumber)) error = "Строка номер телефона не может быть пустой";
        if (string.IsNullOrEmpty(email)) error = "Строка почты не может быть пустой";
        if (string.IsNullOrEmpty(address)) error = "Строка адреса не может быть пустой";

        var parent = new Parent(id, firstName, lastName, patronymic, dateOfBirth, role, phoneNumber, email, address,  pupilId, pupil);
        
        return (parent, error);
    }
}