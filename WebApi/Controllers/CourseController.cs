using Microsoft.AspNetCore.Mvc;
using Domain.Responses;
using Infrastructure.Interfaces;
using Domain.DTOs.StudentDto;
using Domain.Filters;
using Domain.DTOs.CourseDto;



namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService courseService)
{

    [HttpGet]
    public Task<Response<List<GetCourseDto>>> GetAll(StudentFilter filter)
    {
        return courseService.GetAll(filter);
    }

    [HttpPost]
    public Task<Response<GetCourseDto>> Add(CreateCourseDto courseDto)
    {
        return courseService.AddCourse(courseDto);

    }

    [HttpGet("{id:int}")]
    public Task<Response<GetCourseDto>> GetById(int id)
    {
        return courseService.GetCourseById(id);
    }



    [HttpPut]
    public Task<Response<GetCourseDto>> Update(int id, UpdateCourseDto courseDto)
    {
        return courseService.Update(id, courseDto);

    }


    [HttpDelete("{id:int}")]
    public Task<Response<string>> Delete(int id)
    {
        return courseService.DeleteCourse(id);

    }


    [HttpGet("student-count-per-course")]
    public Task<Response<List<CourseStudentCountDto>>> GetStudentCountPerCourse()
    {
        return courseService.GetStudentCountPerCourse();
    }


    [HttpGet("average-grade-per-course")]
    public Task<Response<List<CourseAverageGradeDto>>> GetAverageGradePerCourse()
    {
        return courseService.GetCoursesAverageGrades();
    }


}
