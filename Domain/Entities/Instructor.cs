using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Instructor
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public List<CourseAssignment> CourseAssignments { get; set; }
}