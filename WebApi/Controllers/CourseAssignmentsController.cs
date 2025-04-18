using Microsoft.AspNetCore.Mvc;
using Domain.Responses;
using Infrastructure.Interfaces;
using Domain.DTOs.StudentDto;
using Domain.Filters;
using Domain.DTOs.CourseDto;
using Domain.DTOs.CourseAssignmentServiceDto;
using Domain.DTOs.CourseAssignmentDto;



namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CourseAssignmentsController(ICourseAssignmentService courseAssignmentService)
{

    [HttpGet]
    public Task<Response<List<GetCourseAssignmentDto>>> GetAll(StudentFilter filter)
    {
        return courseAssignmentService.GetAll(filter);
    }

    [HttpPost]
    public Task<Response<GetCourseAssignmentDto>> Add(CreateCourseAssignment courseAssignment)
    {
        return courseAssignmentService.Add(courseAssignment);

    }

    [HttpGet("{id:int}")]
    public Task<Response<GetCourseAssignmentDto>> GetById(int id)
    {
        return courseAssignmentService.GetById(id);
    }



    [HttpPut]
    public Task<Response<GetCourseAssignmentDto>> Update(int id, UpdateCourseAssignment courseAssignment)
    {
        return courseAssignmentService.Update(id, courseAssignment);

    }


    [HttpDelete("{id:int}")]
    public Task<Response<string>> Delete(int id)
    {
        return courseAssignmentService.Delete(id);

    }

}
