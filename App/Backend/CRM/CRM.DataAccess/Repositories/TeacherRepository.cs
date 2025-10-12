using AutoMapper;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class TeacherRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;
    
    public TeacherRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Teacher>> GetAllTeachers()
    {
        var teacherEntity = await _context.Teachers
            .Include(t => t.Subjects)
            .AsNoTracking()
            .ToListAsync();
        
        var teachers = _mapper.Map<List<Teacher>>(teacherEntity).ToList();
        return teachers;
    }

    public async Task<Guid> AddTeacher(Teacher teacher)
    {
        var teacherEntity = _mapper.Map<TeacherEntity>(teacher);

        await _context.AddAsync(teacherEntity);
        await _context.SaveChangesAsync();
        
        return teacherEntity.Id;
    }

    public async Task<Guid> UpdateTeacher(Guid id, string firstName, string lastName, string patronymic,
        DateOnly dateOfBirth,
        string photoPath, string phoneNumber, string email, string address, ICollection<Subject> subjects)
    {
        var teacherEntity = await _context.Teachers
            .Include(t => t.Subjects)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (teacherEntity == null)
            throw new Exception("Учитель не найден");

        teacherEntity.FirstName = firstName;
        teacherEntity.LastName = lastName;
        teacherEntity.Patronymic = patronymic;
        teacherEntity.DateOfBirth = dateOfBirth;
        teacherEntity.PhotoPath = photoPath;
        teacherEntity.PhoneNumber = phoneNumber;
        teacherEntity.Email = email;
        teacherEntity.Address = address;

        _context.Subjects.RemoveRange(teacherEntity.Subjects);

        var subjectEntity = subjects.Select(s => _mapper.Map<SubjectEntity>(s)).ToList();
        teacherEntity.Subjects = subjectEntity;

        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<Guid> DeleteTeacher(Guid id)
    {
        var teacherEntity = await _context.Teachers.Where(t => t.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}