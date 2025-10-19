using CRM.DataAccess.Configurations;
using CRM.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess;

public class CRMContext : IdentityDbContext<UserEntity>
{
    public CRMContext(DbContextOptions<CRMContext> options) : base(options)
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
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ClubConfiguration());
        modelBuilder.ApplyConfiguration(new ClubEnrollmentCongiguration());
        modelBuilder.ApplyConfiguration(new ClubPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new NewsConfiguration());
        modelBuilder.ApplyConfiguration(new ChatRoomConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
        
        var adminId = Guid.NewGuid();

        var admin = new UserEntity
        {
            Id = adminId,
            UserName = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin321"),
            Role = "Admin"
        };

        modelBuilder.Entity<UserEntity>().HasData(admin);
    }
    
    public DbSet<DirectorateEntity> Directorates { get; set; }
    public DbSet<GradeEntity> Grades { get; set; }
    public DbSet<ParentEntity> Parents { get; set; }
    public DbSet<PupilEntity> Pupils { get; set; }
    public DbSet<SubjectEntity> Subjects { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<ClubEntity> Clubs { get; set; }
    public DbSet<ClubEnrollmentEntity> ClubEnrollments { get; set; }
    public DbSet<ClubPaymentEntity> ClubPayment { get; set; }
    public DbSet<EventEntity> Events { get; set; }
    public DbSet<NewsEntity> News { get; set; }
    public DbSet<ChatRoomEntity> ChatRooms { get; set; }
    public DbSet<ChatMessageEntity> ChatMessages { get; set; }
}