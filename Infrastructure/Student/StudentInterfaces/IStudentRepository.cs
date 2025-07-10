namespace Infrastructure.Student.StudentInterfaces;

public interface IStudentRepository
{
    Task<int> CreateAsync(Domain.Entites.Student student);
}
