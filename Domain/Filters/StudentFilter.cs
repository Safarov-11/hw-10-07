using Domain.DTOs;
using Domain.DTOs.StudentDTO;
using Domain.Paginations;

namespace Domain.Filters;

public class StudentFilter : ValidFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? GradeHigherThan { get; set; }
    public int? GradeLoverThan { get; set; }

}
