using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Enrollment
{
    [Key]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollDate { get; set; }
    public int Grade { get; set; }
    public List<Student> Students { get; set; }
    public List<Course> Courses { get; set; }
}