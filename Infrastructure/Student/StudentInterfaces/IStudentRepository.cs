using Domain.ApiResponse;
using Domain.DTOs.StudentDTO;
using Domain.Filters;

namespace Infrastructure.Student.StudentInterfaces;

public interface IStudentRepository
{
    Task<int> CreateAsync(Domain.Entites.Student student);
    Task<PagedResponse<List<Domain.Entites.Student>>> GetAllAsync(StudentFilter filter);
    Task<int> UpdateAsync(Domain.Entites.Student student);
    Task<int> DeleteAsync(Domain.Entites.Student student);
    Task<GetStudentDTO> GetAsync(int id);

}
