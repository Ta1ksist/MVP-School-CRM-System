using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class PupilService : IPupilService 
{
    private readonly IPupilRepository _repository;
    private readonly IMapper _mapper;

    public PupilService(IPupilRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Pupil> GetPupilByName(string firstName, string lastName)
    {
        return await _repository.GetPupilByName(firstName, lastName);
    }

    public async Task<List<Pupil>> GetAllPupils()
    {
        return await _repository.GetAllPupils();
    }

    public async Task<List<string>> GetEmailsAllPupils()
    {
        return await _repository.GetEmailsAllPupils();
    }

    public async Task<Guid> AddPupil(Pupil pupil)
    {
        return await _repository.AddPupil(pupil);
    }

    public async Task<Guid> UpdatePupil(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        Guid gradeId, Grade grade, string phoneNumber, string email, string address, ICollection<Parent> parents)
    {
        return await _repository.UpdatePupil(id, firstName, lastName, patronymic, dateOfBirth, gradeId, grade, 
            phoneNumber, email, address, parents);
    }

    public async Task<Guid> DeletePupil(Guid id)
    {
        return await _repository.DeletePupil(id);
    }
}
