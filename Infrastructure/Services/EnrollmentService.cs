using System.Net;
using AutoMapper;
using Domain.DTOs.EnrollmentDto;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EnrollmentService(DataContext context, IMapper mapper)
{
    public async Task<Response<GetEnrollmentDto>> AddEnrollment(CreateEnrrollmentDto enrrollmentDto)
    {
        var enrollment = mapper.Map<Enrollment>(enrrollmentDto);
       

        await context.Enrollments.AddAsync(enrollment);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetEnrollmentDto>(enrollment);

        return result == 0
            ? new Response<GetEnrollmentDto>(HttpStatusCode.BadRequest, "Enrollment not added!")
            : new Response<GetEnrollmentDto>(data);
    }

    public async Task<Response<string>> DeleteEnrollment(int id)
    {
        var exist = await context.Enrollments.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Enrollment not found");
        }

        context.Enrollments.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Enrollment not deleted!")
            : new Response<string>("Enrollment deleted!");
    }

    public async Task<Response<List<GetEnrollmentDto>>> GetAll()
    {
        var enrollments = await context.Enrollments.ToListAsync();

        var data = mapper.Map<List<GetEnrollmentDto>>(enrollments);
        return new Response<List<GetEnrollmentDto>>(data);
    }

    public async Task<Response<GetEnrollmentDto>> GetEnrollmentById(int id)
    {
        var exist = await context.Enrollments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetEnrollmentDto>(HttpStatusCode.NotFound, "Enrollment not found!");
        }

        var data = mapper.Map<GetEnrollmentDto>(exist);

        return new Response<GetEnrollmentDto>(data);
    }

    public async Task<Response<GetEnrollmentDto>> Update(int id, UpdateEnrollmentDto enrollmentDto)
    {
        var exist = await context.Enrollments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetEnrollmentDto>(HttpStatusCode.NotFound, "Enrollment not found");
        }

        exist.StudentId = enrollmentDto.StudentId;
        exist.CourseId = enrollmentDto.CourseId;
        exist.EnrollDate = enrollmentDto.EnrollDate;
        exist.Grade = enrollmentDto.Grade;
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetEnrollmentDto>(exist);

        return result == 0
            ? new Response<GetEnrollmentDto>(HttpStatusCode.BadRequest, "Enrollment not updated")
            : new Response<GetEnrollmentDto>(data);
    }
}
