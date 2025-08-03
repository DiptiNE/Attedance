using AutoMapper;
using ParentTeacherBridge.API.DTOs;
using ParentTeacherBridge.API.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Attendance Mapping
        CreateMap<EventCreateDto, Event>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        
        CreateMap<EventUpdateDto, Event>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        
        CreateMap<Event, EventDto>();


        //Teacher Mapping
        CreateMap<Teacher, TeacherDTO>();
        CreateMap<CreateTeacherDto, Teacher>()
            .ForMember(dest => dest.Created_At, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Updated_At, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateTeacherDto, Teacher>()
            .ForMember(dest => dest.Updated_At, opt => opt.MapFrom(src => DateTime.UtcNow));

        //Attendance

        // Entity -> DTO
        CreateMap<Attendance, AttendanceDto>()
            .ForMember(dest => dest.Student_Id, opt => opt.MapFrom(src => src.Student_Id))
            .ForMember(dest => dest.Class_Id, opt => opt.MapFrom(src => src.Class_Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date ?? default));

        // Create DTO -> Entity
        CreateMap<CreateAttendanceDto, Attendance>()
            .ForMember(dest => dest.Attendance_Id, opt => opt.Ignore())
            .ForMember(dest => dest.Student_Id, opt => opt.Ignore()) // set from route
            .ForMember(dest => dest.Class_Id, opt => opt.MapFrom(src => src.Class_Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Update DTO -> Entity
        CreateMap<UpdateAttendanceDto, Attendance>()
            .ForMember(dest => dest.Student_Id, opt => opt.Ignore()) // ensure route studentId
            .ForMember(dest => dest.Class_Id, opt => opt.MapFrom(src => src.Class_Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }


}
