namespace CRM.API.Contracts.Responses;

public record PupilDebDTOResponse(
    Guid PupilId,
    string PupilFirstName,
    string PupilLastName,
    string PupilGrade,
    decimal TotalExpectedAmount,
    decimal TotalPaidAmount
    );