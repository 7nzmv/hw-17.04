using Microsoft.AspNetCore.Mvc;
using Domain.Responses;
using Infrastructure.Interfaces;
using Domain.DTOs.StudentDto;
using Domain.Filters;
using Domain.DTOs.CourseDto;
using Domain.DTOs.EnrollmentDto;



namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController(IEnrollmentService enrollmentService)
{

    [HttpGet]
    public Task<Response<List<GetEnrollmentDto>>> GetAll(StudentFilter filter)
    {
        return enrollmentService.GetAll(filter);
    }

    [HttpPost]
    public Task<Response<GetEnrollmentDto>> Add(CreateEnrrollmentDto enrrollmentDto)
    {
        return enrollmentService.AddEnrollment(enrrollmentDto);

    }

    [HttpGet("{id:int}")]
    public Task<Response<GetEnrollmentDto>> GetById(int id)
    {
        return enrollmentService.GetEnrollmentById(id);
    }



    [HttpPut]
    public Task<Response<GetEnrollmentDto>> Update(int id, UpdateEnrollmentDto enrollmentDto)
    {
        return enrollmentService.Update(id, enrollmentDto);

    }


    [HttpDelete("{id:int}")]
    public Task<Response<string>> Delete(int id)
    {
        return enrollmentService.DeleteEnrollment(id);

    }

}
