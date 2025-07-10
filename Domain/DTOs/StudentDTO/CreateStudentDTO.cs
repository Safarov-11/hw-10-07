namespace Domain.DTOs.StudentDTO;

public class CreateStudentDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DOB { get; set; }
    public int Grade { get; set; }
}
