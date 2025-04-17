using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class CourseAssignment
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int InstructorId { get; set; }
    public List<Course> Courses { get; set; }
    public Instructor Instructor { get; set; }
}