using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _repository;
    private readonly IMapper _mapper;
    
    public SubjectService(ISubjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Subject> GetSubjectByName(string name)
    {
        return await _repository.GetSubjectByName(name);
    }
    
    public async Task<List<Subject>> GetAllSubjects()
    {
        return await _repository.GetAllSubjects();
    }

    public async Task<Guid> AddSubject(Subject subject)
    {
        return await _repository.AddSubject(subject);
    }

    public async Task<Guid> UpdateSubject(Guid id, string name, Teacher teachers)
    {
        return await _repository.UpdateSubject(id, name, teachers);
    }

    public async Task<Guid> DeleteSubject(Guid id)
    {
        return await _repository.DeleteSubject(id);
    }
}