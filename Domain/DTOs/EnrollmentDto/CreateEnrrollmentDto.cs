namespace Domain.DTOs.EnrollmentDto;

public class CreateEnrrollmentDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollDate { get; set; }
    public int Grade { get; set; }
}
