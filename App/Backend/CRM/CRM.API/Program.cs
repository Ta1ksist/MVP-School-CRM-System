using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CRM.API.Data;
using CRM.API.Hubs;
using CRM.Application.Mappings;
using CRM.Application.Services;
using CRM.Application.Settings;
using CRM.Core.Abstractions.Auth;
using CRM.Core.Abstractions.Repositories;
using CRM.Core.Abstractions.Services;
using CRM.DataAccess;
using CRM.DataAccess.Entities;
using CRM.DataAccess.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Введите токен в формате: Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<CRMContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(connectionString, new PostgreSqlStorageOptions
    {
        SchemaName = "hangfire"
    }));
builder.Services.AddHangfireServer();
builder.Services.AddSignalR();

builder.Services.AddScoped<IDirectorateRepository, DirectorateRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IPupilRepository, PupilRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClubRepository, СlubRepository>();
builder.Services.AddScoped<IClubEnrollmentRepository, ClubEnrollmentRepository>();
builder.Services.AddScoped<IClubPaymentRepository, ClubPaymentRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

builder.Services.AddScoped<IDirectorateService, DirectorateService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<IPupilService, PupilService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IClubEnrollmentService, ClubEnrollmentService>();
builder.Services.AddScoped<IClubPaymentService, ClubPaymentService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IFinancialReportService, FinancialReportService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IScheduledTasksService, ScheduledTasksService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.SaveToken = true;
//         options.RequireHttpsMetadata = false;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             RoleClaimType = "role", // ✅ для ролей
//             NameClaimType = "name",
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//         };
//         options.Events = new JwtBearerEvents
//         {
//             OnMessageReceived = context =>
//             {
//                 var accessToken = context.Request.Query["access_token"];
//                 if (!string.IsNullOrEmpty(accessToken) &&
//                     context.HttpContext.Request.Path.StartsWithSegments("/chathub"))
//                 {
//                     context.Token = accessToken;
//                 }
//                 return Task.CompletedTask;
//             }
//         };
//     });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",builder => 
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CRMContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<RoleManager<IdentityRole<Guid>>>();
builder.Services.AddScoped<UserManager<UserEntity>>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await DataSeeder.SeedRolesAsync(roleManager);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "CRM Dashboard",
    Authorization = new[] { new HangfireAuthorizationFilterService() }
});
RecurringJob.AddOrUpdate<ScheduledTasksService>(
    methodCall: service => service.SendMonthlyIncomeReport(),
    cronExpression: Cron.Monthly(1, 8)
);

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/hangfire");
app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();