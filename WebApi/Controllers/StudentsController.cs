using Microsoft.AspNetCore.Mvc;
using Domain.Responses;
using Infrastructure.Interfaces;
using Domain.DTOs.StudentDto;
using Domain.Filters;
using Domain.DTOs.CourseDto;



namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService)
{

    [HttpGet]
    public Task<Response<List<GetStudentDto>>> GetAllStudent(StudentFilter filter)
    {
        return studentService.GetAll(filter);
    }

    [HttpPost]
    public Task<Response<GetStudentDto>> AddStudent(CreateStudentDto studentDto)
    {
        return studentService.AddStudent(studentDto);

    }

    [HttpGet("{id:int}")]
    public Task<Response<GetStudentDto>> GetStudentById(int id)
    {
        return studentService.GetStudentById(id);
    }



    [HttpPut]
    public Task<Response<GetStudentDto>> UpdateStudent(int id, UpdateStudentDto studentDto)
    {
        return studentService.Update(id, studentDto);

    }


    [HttpDelete("{id:int}")]
    public Task<Response<string>> DeleteStudent(int id)
    {
        return studentService.DeleteStudent(id);

    }

    [HttpGet("course-count-per-student")]
    public Task<Response<List<StudentCourseCountDto>>> GetCourseCountPerStudent()
    {
        return studentService.GetCourseCountPerStudent();

    }


    [HttpGet("students-without-courses")]
    public Task<Response<List<StudentWithoutCoursesDto>>> GetStudentsWithoutCourses()
    {
        return studentService.GetStudentsWithoutCourses();
    }




}
