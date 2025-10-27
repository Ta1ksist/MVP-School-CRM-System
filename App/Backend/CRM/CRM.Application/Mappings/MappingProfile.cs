using AutoMapper;
using CRM.Core.Models;
using CRM.DataAccess.Entities;

namespace CRM.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DirectorateEntity, Directorate>();
        CreateMap<GradeEntity, Grade>();
        CreateMap<ParentEntity, Parent>();
        CreateMap<PupilEntity, Pupil>();
        CreateMap<SubjectEntity, Subject>();
        CreateMap<TeacherEntity, Teacher>();
        CreateMap<UserEntity, User>();
        CreateMap<ClubEntity, Club>();
        CreateMap<ClubEnrollmentEntity, ClubEnrollment>();
        CreateMap<ClubPaymentEntity, ClubPayment>();
        CreateMap<EventEntity, Event>();
        CreateMap<NewsEntity, News>();
        CreateMap<ChatRoomEntity, ChatRoom>();
        CreateMap<ChatMessageEntity, ChatMessage>();
    }
}