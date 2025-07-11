using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.StudentDTO;
using Domain.Entites;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Data;
using Infrastructure.Interfaces.Student;
using Infrastructure.Interfaces.Student.StudentInterfaces;
using Infrastructure.Student.StudentInterfaces;
using Infrastructure.Student.StudentMappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StudentsService;

public class StudentsService(DataContext context,
 IMapper mapper,
 IStudentRepository studentRepository) : IStudentService
{
    public async Task<Response<string>> AddStudentAsync(CreateStudentDTO studentDTO)
    {
        // var student = mapper.Map<Student>(studentDTO);
        var student = StudentMapper.ToEntity(studentDTO);//new
        // await context.Students.AddAsync(mappedStudent);
        // var res = await context.SaveChangesAsync();
        var res = await studentRepository.CreateAsync(student);//new
        return res == 0
        ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
        : Response<string>.Success(messenge: "Succes");

    }

    public async Task<Response<string>> DeleteStudentAsync(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student == null)
        {
            return Response<string>.Error("Student not found", HttpStatusCode.NotFound);
        }

        var res = await studentRepository.DeleteAsync(student);
        return res == 0
        ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
        : Response<string>.Success(null, "Succes");
        
    }

    public async Task<Response<string>> UpdateStudentAsync(UpdateStudentDTO studentDTO)
    {
        var student = await context.Students.FindAsync(studentDTO.Id);
        if (student == null)
        {
            return Response<string>.Error("Student not found", HttpStatusCode.NotFound);
        }
        if (studentDTO.DOB >= DateTime.Now)
        {
            return Response<string>.Error("Date of birth cant be higher than now", HttpStatusCode.BadRequest);
        }

        student.ToEntity(studentDTO);
        var res = await studentRepository.UpdateAsync(student);
        return res == 0
        ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
        : Response<string>.Success(null, "Succes");

    }

    public async Task<Response<GetStudentDTO?>> GetStudentAsync(int id)
    {
        var student = await context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (student == null)
        {
            return Response<GetStudentDTO?>.Error("Student not found", HttpStatusCode.NotFound);
        }
        var mappedStudent = student.ToDTO();

        return Response<GetStudentDTO?>.Success(mappedStudent);
        
    }


    public async Task<PagedResponse<List<GetStudentDTO>>> GetStudentsAsync(StudentFilter filter)
    {
        var ValidFilter = new ValidFilter(filter.PageSize, filter.PageNumber);

        

        return PagedResponse<List<GetStudentDTO>>.Success(mappedStudents, ValidFilter.PageNumber, ValidFilter.PageSize, totalRecords);

    }


}
