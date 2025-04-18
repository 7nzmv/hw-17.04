using Domain.DTOs.StudentDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Task<Response<GetStudentDto>> AddStudent(CreateStudentDto studentDTO);
    Task<Response<string>> DeleteStudent(int id);
    Task<Response<List<GetStudentDto>>> GetAll(StudentFilter filter);
    Task<Response<GetStudentDto>> GetStudentById(int id);
    Task<Response<GetStudentDto>> Update(int id, UpdateStudentDto studentDTO);
    Task<Response<List<StudentCourseCountDto>>> GetCourseCountPerStudent();
    Task<Response<List<StudentWithoutCoursesDto>>> GetStudentsWithoutCourses();
}
