using Domain.DTOs.CourseDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Task<Response<GetCourseDto>> AddCourse(CreateCourseDto courseDto);
    Task<Response<string>> DeleteCourse(int id);
    Task<Response<List<GetCourseDto>>> GetAll(StudentFilter filter);
    Task<Response<GetCourseDto>> GetCourseById(int id);
    Task<Response<GetCourseDto>> Update(int id, UpdateCourseDto courseDto);
    Task<Response<List<CourseStudentCountDto>>> GetStudentCountPerCourse();
    Task<Response<List<CourseAverageGradeDto>>> GetCoursesAverageGrades();

}
