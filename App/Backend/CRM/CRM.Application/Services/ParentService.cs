using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class ParentService : IParentService
{
    private readonly IParentRepository _repository;
    private readonly IMapper _mapper;
    
    public ParentService(IParentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Parent> GetParentByName(string firstName, string lastName)
    {
        return await _repository.GetParentByName(firstName, lastName);
    }
    
    public async Task<List<Parent>> GetAllParents()
    {
        return await _repository.GetAllParents();
    }

    public async Task<List<string>> GetEmailsAllParents()
    {
        return await _repository.GetEmailsAllParents();
    }
    
    public async Task<Guid> AddParent(Parent parent)
    {
        return await _repository.AddParent(parent);
    }

    public async Task<Guid> UpdateParent(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string role, string phoneNumber, string email, string address, Guid pupilId, Pupil pupil)
    {
        return await _repository.UpdateParent(id, firstName, lastName, patronymic, dateOfBirth, role, phoneNumber,
            email, address, pupilId, pupil);
    }

    public async Task<Guid> DeleteParent(Guid id)
    {
        return await _repository.DeleteParent(id);
    }
}