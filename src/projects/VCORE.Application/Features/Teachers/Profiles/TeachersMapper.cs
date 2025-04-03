using AutoMapper;
using VCORE.Application.Features.Teachers.Commands.Create;
using VCORE.Application.Features.Teachers.Commands.CreateRange;
using VCORE.Application.Features.Teachers.Commands.Update;
using VCORE.Application.Features.Teachers.Commands.UpdateRange;
using VCORE.Application.Features.Teachers.Queries.GetAll;
using VCORE.Application.Features.Teachers.Queries.GetAllDetail;
using VCORE.Application.Features.Teachers.Queries.GetById;
using VCORE.Application.Features.Teachers.Queries.GetByName;
using VCORE.Application.Features.Teachers.Queries.GetDetailById;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Teachers.Profiles;

public class TeachersMapper : Profile
{
    public TeachersMapper()
    {
        CreateMap<TeacherAddCommand, Teacher>();
        CreateMap<TeacherUpdateCommand, Teacher>();
        CreateMap<TeacherAddRangeRequestDto, Teacher>().ReverseMap();
        CreateMap<TeacherUpdateRangeRequestDto, Teacher>();

        CreateMap<Teacher, TeacherGetAllResponseDto>();
        CreateMap<Teacher, TeacherGetByIdResponseDto>();
        CreateMap<Teacher, TeacherGetAllDetailResponseDto>();
        CreateMap<Teacher, TeacherGetByNameResponseDto>();
        CreateMap<Teacher, TeacherGetDetailByIdResponseDto>();

        CreateMap<Teacher, Teacher>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate));
    }
}