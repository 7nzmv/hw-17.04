namespace Domain.DTOs.CourseDto;

public class GetCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
