using AutoMapper;
using VCORE.Application.Features.Teachers.Commands.Create;
using VCORE.Application.Features.Teachers.Commands.Update;
using VCORE.Application.Features.Teachers.Queries.GetAll;
using VCORE.Application.Features.Teachers.Queries.GetById;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Teachers.Profiles;

public class TeachersMapper : Profile
{
    public TeachersMapper()
    {
        CreateMap<TeacherAddCommand, Teacher>();
        CreateMap<TeacherUpdateCommand, Teacher>();
        
        CreateMap<Teacher, TeacherGetAllResponseDto>();
        CreateMap<Teacher, TeacherGetByIdResponseDto>();
    }
}