using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repository;
    private readonly IMapper _mapper;

    public TeacherService(ITeacherRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Teacher> GetTeacherByName(string firstName, string lastName)
    {
        return await _repository.GetTeacherByName(firstName, lastName);
    }
    
    public async Task<List<Teacher>> GetAllTeachers()
    {
        return await _repository.GetAllTeachers();
    }

    public async Task<Guid> AddTeacher(Teacher teacher)
    {
        return await _repository.AddTeacher(teacher);
    }

    public async Task<Guid> UpdateTeacher(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string photoPath, string phoneNumber, string email, string address, ICollection<Subject> subjects, Guid userId)
    {
        return await _repository.UpdateTeacher(id, firstName, lastName, patronymic, dateOfBirth,  photoPath, 
            phoneNumber, email, address, subjects, userId);
    }

    public async Task<Guid> DeleteTeacher(Guid id)
    {
        return await _repository.DeleteTeacher(id);
    }
}