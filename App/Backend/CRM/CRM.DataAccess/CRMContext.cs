using CRM.DataAccess.Configurations;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess;

public class CRMContext : DbContext
{
    public CRMContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DirectorateConfiguration());
        modelBuilder.ApplyConfiguration(new GradeConfiguration());
        modelBuilder.ApplyConfiguration(new ParentConfiguration());
        modelBuilder.ApplyConfiguration(new PupilConfiguration());
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
    }
    
    public DbSet<DirectorateEntity> Directorates { get; set; }
    public DbSet<GradeEntity> Grades { get; set; }
    public DbSet<ParentEntity> Parents { get; set; }
    public DbSet<PupilEntity> Pupils { get; set; }
    public DbSet<SubjectEntity> Subjects { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
}