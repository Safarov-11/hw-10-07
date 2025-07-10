using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Student
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "First name must be added")]
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DOB { get; set; }
    [Range(0,100)]
    public int Grade { get; set; }

}
