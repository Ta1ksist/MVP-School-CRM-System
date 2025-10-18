namespace CRM.Core.Models;

public class Club
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal MonthlyFee { get; set; }
    public bool IsActive { get; set; }

    public ClubEnrollment Enrollments { get; set; }

    public Club(Guid id, string name, string description, decimal monthlyFee, bool isActive,
        ClubEnrollment enrollments)
    {
        Id = id;
        Name = name;
        Description = description;
        MonthlyFee = monthlyFee;
        IsActive = isActive;
        Enrollments = enrollments;
    }

    public static (Club club, string Error) Create(Guid id, string name, string description, decimal monthlyFee,
        bool isActive, ClubEnrollment enrollments)
    {
        string error = "";

        if (string.IsNullOrEmpty(name)) error = "Название кружка/секции не может быть пустым";
        if (string.IsNullOrEmpty(description)) error = "Описание кружка/секции не может быть пустым";
        if (monthlyFee < 0) error = "Ежемесячный платеж кружка/секции не может быть отрицательным";
        
        var club = new Club(id, name, description, monthlyFee, isActive, enrollments);
        
        return (club, error);
    }
}