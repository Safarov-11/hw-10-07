using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.StudentDTO;
using Domain.Filters;

namespace Infrastructure.Interfaces.Student.StudentInterfaces;

public interface IStudentService
{
    Task<Response<string>> AddStudentAsync(CreateStudentDTO studentDTO);
    Task<Response<string>> UpdateStudentAsync(UpdateStudentDTO studentDTO);
    Task<Response<string>> DeleteStudentAsync(int id);
    Task<Response<GetStudentDTO?>> GetStudentAsync(int id);
    Task<PagedResponse<List<GetStudentDTO>>> GetStudentsAsync(StudentFilter filter);
}
