using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.Core.Models;

namespace CRM.Application.Services;

public class DirectorateService : IDirectorateService
{
    private readonly IDirectorateRepository _repository;
    private readonly IMapper _mapper;

    public DirectorateService(IDirectorateRepository directorateRepository, IMapper mapper)
    {
        _repository = directorateRepository;
        _mapper = mapper;
    }

    public async Task<Directorate> GetDirectorateByName(string firstName, string lastName)
    {
        return await _repository.GetDirectorateByName(firstName, lastName);
    }
    
    public async Task<List<Directorate>> GetAllDirectorates()
    {
        return await _repository.GetAllDirectorates();
    }

    public async Task<List<string>> GetEmailsAllDirectorates()
    {
        return await _repository.GetEmailsAllDirectorates();
    }
    
    public async Task<Guid> AddDirectorate(Directorate directorate)
    {
        return await _repository.AddDirectorate(directorate);
    }

    public async Task<Guid> UpdateDirectorate(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth, string photoPath, string role, string phoneNumber, string email, string address, Guid userId)
    {
        return await _repository.UpdateDirectorate(id, firstName, lastName, patronymic, dateOfBirth,  photoPath,
            role, phoneNumber, email, address, userId);
    }

    public async Task<Guid> DeleteDirectorate(Guid id)
    {
        return await _repository.DeleteDirectorate(id);
    }
}