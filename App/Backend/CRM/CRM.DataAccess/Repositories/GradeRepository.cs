using AutoMapper;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Models;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly CRMContext _context;
    private readonly IMapper _mapper;

    public GradeRepository(CRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Grade> GetGradeByName(string name)
    {
        var gradeEntity = await _context.Grades
            .Where(g => g.Name == name)
            .FirstOrDefaultAsync();
        var grade = _mapper.Map<Grade>(gradeEntity);
        
        return grade;
    }
    
    public async Task<List<Grade>> GetAllGrades()
    {
        var gradeEntity = await _context.Grades
            .Include(g => g.Pupils)
            .AsNoTracking()
            .ToListAsync();
        
        var grades = _mapper.Map<List<Grade>>(gradeEntity).ToList();
        return grades;
    }

    public async Task<Guid> AddGrade(Grade grade)
    {
        var gradeEntity = _mapper.Map<GradeEntity>(grade);
        gradeEntity.Id = Guid.NewGuid();
        
        await _context.Grades.AddAsync(gradeEntity);
        await _context.SaveChangesAsync();
        
        return gradeEntity.Id;
    }

    public async Task AddPupilToGrade(Guid gradeId, Guid pupilId)
    {
        var ge = await _context.Grades.Include(g => g.Pupils).FirstOrDefaultAsync(g => g.Id == gradeId);
        if (ge == null) throw new KeyNotFoundException("Класс не найден");

        var pe = await _context.Pupils.FirstOrDefaultAsync(p => p.Id == pupilId);
        if (pe == null) throw new KeyNotFoundException("Ученик не найден");

        pe.GradeId = gradeId;
        if (!ge.Pupils.Any(p => p.Id == pupilId)) ge.Pupils.Add(pe);

        await _context.SaveChangesAsync();
    }
    
    public async Task<Guid> UpdateGrade(Guid id, string name)
    {
        var gradeEntity = await _context.Grades
            .FirstOrDefaultAsync(g => g.Id == id);

        if (gradeEntity == null) throw new Exception("Класс не найден");

        gradeEntity.Name = name;

        await _context.SaveChangesAsync();
        return id;
    }

    public async Task<Guid> DeleteGrade(Guid id)
    {
        var gradeEntity = await _context.Grades.Where(g => g.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
    
    public async Task RemovePupilFromGrade(Guid gradeId, Guid pupilId)
    {
        var pe = await _context.Pupils.FirstOrDefaultAsync(p => p.Id == pupilId && p.GradeId == gradeId);
        if (pe == null) throw new KeyNotFoundException("Ученик в классе не найден");

        pe.GradeId = Guid.Empty;
        await _context.SaveChangesAsync();
    }
}