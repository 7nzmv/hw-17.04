using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }   
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<Enrollment> Enrollments { get; set; }
}