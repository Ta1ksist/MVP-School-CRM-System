using AutoMapper;
using CRM.Core.Models;
using CRM.DataAccess.Entities;

namespace CRM.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DirectorateEntity, Directorate>().ReverseMap();;
        CreateMap<GradeEntity, Grade>().ReverseMap();;
        CreateMap<ParentEntity, Parent>().ReverseMap();;
        CreateMap<PupilEntity, Pupil>().ReverseMap();;
        CreateMap<SubjectEntity, Subject>().ReverseMap();;
        CreateMap<TeacherEntity, Teacher>().ReverseMap();;
        CreateMap<UserEntity, User>()
            .ConstructUsing(src =>
                User.Create(src.Id, src.UserName, src.PasswordHash, src.Role, src.TeacherId, null, src.DirectorateId,
                    null).user).ReverseMap();;
        CreateMap<User, UserEntity>()
            .ForMember(dest => dest.Teacher, opt => opt.Ignore())
            .ForMember(dest => dest.Directorate, opt => opt.Ignore()).ReverseMap();;
        CreateMap<ClubEntity, Club>().ReverseMap();;
        CreateMap<ClubEnrollmentEntity, ClubEnrollment>().ReverseMap();;
        CreateMap<ClubPaymentEntity, ClubPayment>().ReverseMap();;
        CreateMap<EventEntity, Event>().ReverseMap();;
        CreateMap<NewsEntity, News>().ReverseMap();;
        CreateMap<ChatRoomEntity, ChatRoom>()
            .ForMember(dest => dest.Participants,
                opt => opt
                    .MapFrom(src => src.Participants.Select(p => p.UserId))).ReverseMap();;
        CreateMap<ChatRoom, ChatRoomEntity>()
            .ForMember(dest => dest.Participants,
                opt => opt
                    .MapFrom(src => src.Participants
                    .Select(id => new ChatParticipantEntity { UserId = id }))).ReverseMap();;
        CreateMap<ChatMessageEntity, ChatMessage>().ReverseMap();;
        CreateMap<ChatParticipantEntity, ChatParticipant>().ReverseMap();;
    }
}