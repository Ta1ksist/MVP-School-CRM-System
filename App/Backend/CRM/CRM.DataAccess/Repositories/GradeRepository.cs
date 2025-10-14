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

        await _context.Grades.AddAsync(gradeEntity);
        await _context.SaveChangesAsync();
        
        return gradeEntity.Id;
    }

    public async Task<Guid> UpdateGrade(Guid id, string name, Pupil pupil)
    {
        var gradeEntity = await _context.Grades
            .Include(g => g.Pupils)
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (gradeEntity == null) throw new Exception("Класс не найден");
        
        gradeEntity.Name = name;
        var pupilEntity = _mapper.Map<PupilEntity>(pupil);
        pupilEntity.GradeId = gradeEntity.Id;
        
        if (!gradeEntity.Pupils.Any(p => p.Id == pupilEntity.Id))
        {
            gradeEntity.Pupils.Add(pupilEntity);
        }

        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<Guid> DeleteGrade(Guid id)
    {
        var gradeEntity = await _context.Grades.Where(g => g.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}