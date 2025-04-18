using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Student
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<Enrollment> Enrollments { get; set; }
}