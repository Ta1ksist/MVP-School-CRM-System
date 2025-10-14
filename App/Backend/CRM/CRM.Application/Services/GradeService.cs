using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _repository;
    private readonly IMapper _mapper;
    
    public GradeService(IGradeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Grade> GetGradeByName(string name)
    {
        return await _repository.GetGradeByName(name);
    }
    
    public async Task<List<Grade>> GetAllGrades()
    {
        return await _repository.GetAllGrades();
    }

    public async Task<Guid> AddGrade(Grade grade)
    {
        return await _repository.AddGrade(grade);
    }

    public async Task<Guid> UpdateGrade(Guid id, string name, Pupil pupil)
    {
        return await _repository.UpdateGrade(id, name, pupil);
    }

    public async Task<Guid> DeleteGrade(Guid id)
    {
        return await _repository.DeleteGrade(id);
    }
}