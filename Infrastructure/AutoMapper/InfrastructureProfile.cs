using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.StudentDTO;
using Domain.Entites;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Domain.Entites.Student, GetStudentDTO>().ReverseMap();
        CreateMap<Domain.Entites.Student, UpdateStudentDTO>().ReverseMap();
        CreateMap<Domain.Entites.Student, CreateStudentDTO>().ReverseMap();
    }
}
