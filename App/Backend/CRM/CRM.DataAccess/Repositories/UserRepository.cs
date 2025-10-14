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
    
    public async Task<User> GetUserById(Guid id)
    {
        var userEntity = await _context.Users
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null) return null;
        
        var user = _mapper.Map<User>(userEntity);
        return user;
    }

    public async Task<User> GetUserByUsername(string userName)
    {
        var userEntity = await _context.Users
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.UserName == userName);

        if (userEntity == null) return null;

        var user = _mapper.Map<User>(userEntity);
        return user;
    }
    
    public async Task<User> GetUserTeacherId(Guid teacherId)
    {
        var userEntity = await _context.Users
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.TeacherId == teacherId);
        
        if (userEntity == null) return null;
        
        var userTeacher = _mapper.Map<User>(userEntity);
        
        return  userTeacher;
    }

    public async Task<User> GetUserDirectorateId(Guid directorateId)
    {
        var userEntity = await _context.Users
            .Include(u => u.Teacher)
            .Include(u => u.Directorate)
            .FirstOrDefaultAsync(u => u.DirectorateId == directorateId);
        
        if (userEntity == null) return null;
        
        var userDirectorate = _mapper.Map<User>(userEntity);
        
        return userDirectorate;
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        var userEntity = await _context.Users.AsNoTracking().ToListAsync();
        var users = _mapper.Map<List<User>>(userEntity);

        return users;
    }
    
    public async Task<Guid> AddUser(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        
        return userEntity.Id;
    }
    
    public async Task<Guid> UpdateUser(Guid id, string userName, string passwordHash, string role)
    {
        var userEntity = await _context.Users.Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.UserName, userName)
                .SetProperty(u => u.PasswordHash, passwordHash)
                .SetProperty(u => u.Role, role));

        return id;
    }
    
    public async Task<Guid> DeleteUser(Guid id)
    {
        var userEntity = await _context.Users.Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}