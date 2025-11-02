using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;

    public SubjectRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Subject> GetSubjectByName(string name)
    {
        var subjectEntity = await _context.Subjects
            .Where(s => s.Name == name)
            .FirstOrDefaultAsync();
        var subject = _mapper.Map<SubjectEntity, Subject>(subjectEntity);
        
        return subject;
    }
    
    public async Task<List<Subject>> GetAllSubjects()
    {
        var subjectsEntity = await _context.Subjects
            .Include(s => s.Teachers)
            .AsNoTracking()
            .ToListAsync();
        
        var subjects = _mapper.Map<List<Subject>>(subjectsEntity).ToList();
        return subjects;
    }

    public async Task<Guid> AddSubject(Subject subject)
    {
        var subjectEntity = _mapper.Map<SubjectEntity>(subject);

        await _context.Subjects.AddAsync(subjectEntity);
        await _context.SaveChangesAsync();
        
        return subject.Id;
    }

    public async Task AddTeacherToSubject(Guid subjectId, Guid teacherId)
    {
        var subject = await _context.Subjects
            .Include(s => s.Teachers)
            .FirstOrDefaultAsync(s => s.Id == subjectId);

        if (subject == null) throw new KeyNotFoundException("Предмет не найден");

        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);

        if (teacher == null) throw new KeyNotFoundException("Учитель не найден");

        if (!subject.Teachers.Any(t => t.Id == teacherId))
            subject.Teachers.Add(teacher);

        await _context.SaveChangesAsync();
    }
    
    public async Task<Guid> UpdateSubject(Guid id, string name, ICollection<Teacher> teachers)
    {
        var subjectEntity = await _context.Subjects
            .Include(s => s.Teachers)
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (subjectEntity == null) throw new Exception("Предмет не найден");
        
        subjectEntity.Name = name;
        var teacherEntity = await _context.Teachers.FindAsync(id);
        if (teacherEntity == null)
            throw new Exception("Учитель не найден");

        if (!subjectEntity.Teachers.Any(t => t.Id == teacherEntity.Id))
        {
            subjectEntity.Teachers.Add(teacherEntity);
        }

        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<Guid> DeleteSubject(Guid id)
    {
        var subjectEntity = await _context.Subjects.Where(s => s.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}