using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public UserRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<User?> GetUserById(Guid id)
    {
        var entity = await _context.Users
            .AsSplitQuery()
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.Id == id);

        return entity == null ? null : _mapper.Map<User>(entity);
    }

    public async Task<User?> GetUserByUsername(string userName)
    {
        var entity = await _context.Users
            .AsSplitQuery()
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.UserName == userName);

        return entity == null ? null : _mapper.Map<User>(entity);
    }
    
    public async Task<User?> GetUserTeacherId(Guid teacherId)
    {
        var entity = await _context.Users
            .AsSplitQuery()
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.TeacherId == teacherId);

        return entity == null ? null : _mapper.Map<User>(entity);
    }

    public async Task<User?> GetUserDirectorateId(Guid directorateId)
    {
        var entity = await _context.Users
            .AsSplitQuery()
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.DirectorateId == directorateId);

        return entity == null ? null : _mapper.Map<User>(entity);
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        var entities = await _context.Users
            .AsSplitQuery()
            .ToListAsync();

        return _mapper.Map<List<User>>(entities);
    }
    
    public async Task<Guid> AddUser(User user)
    {
        var entity = _mapper.Map<UserEntity>(user);

        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }
    
    public async Task<Guid> UpdateUser(Guid id, string userName, string passwordHash, string role)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Пользователь не найден");

        entity.UserName = userName;
        entity.PasswordHash = passwordHash;
        entity.Role = role;

        await _context.SaveChangesAsync();
        return entity.Id;
    }
    
    public async Task<Guid> DeleteUser(Guid id)
    {
        var userEntity = await _context.Users.Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        return id;
    }
}