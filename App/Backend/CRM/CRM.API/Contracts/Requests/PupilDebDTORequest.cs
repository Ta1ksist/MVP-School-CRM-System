namespace CRM.API.Contracts.Requests;

public record PupilDebDTORequest(
    Guid PupilId,
    string PupilFirstName,
    string PupilLastName,
    string PupilGrade,
    decimal TotalExpectedAmount,
    decimal TotalPaidAmount
    );