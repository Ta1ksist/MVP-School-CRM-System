using CRM.Core.Models;

namespace CRM.API.Contracts.Requests;

public record GradeRequest(
    string Name,
    ICollection<Pupil> Pupils
    );