namespace CRM.Core.DTOs;

public class MonthlyClubIncomeDTO
{
    public string ClubName { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalIncome { get; set; }
}