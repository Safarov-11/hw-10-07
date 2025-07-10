using Domain.DTOs.StudentDTO;
using Domain.Entites;

namespace Infrastructure.Student.StudentMappers;

public static class StudentMapper
{
    public static Domain.Entites.Student ToEntity(CreateStudentDTO studentDTO)
    {
        return new Domain.Entites.Student
        {
            FirstName = studentDTO.FirstName,
            LastName = studentDTO.LastName,
            DOB = studentDTO.DOB,
            Grade = studentDTO.Grade
        };
    }

    public static void ToEntity(this Domain.Entites.Student student, UpdateStudentDTO studentDTO)
    {
        student.FirstName = studentDTO.FirstName;
        student.LastName = studentDTO.LastName;
        student.DOB = studentDTO.DOB;
        student.Grade = studentDTO.Grade;
    }

    public static List<GetStudentDTO> ToDTO(List<Domain.Entites.Student> students)
    {
        return students.Select(s => new GetStudentDTO
        {
            FirstName = s.FirstName,
            LastName = s.LastName,
            DOB = s.DOB,
            Grade = s.Grade
        }).ToList();
    }

}
