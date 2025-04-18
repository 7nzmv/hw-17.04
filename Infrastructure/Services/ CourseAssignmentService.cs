using System.Net;
using AutoMapper;
using Domain.DTOs.CourseAssignmentDto;
using Domain.DTOs.CourseAssignmentServiceDto;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseAssignmentService(DataContext context, IMapper mapper) : ICourseAssignmentService
{
    public async Task<Response<GetCourseAssignmentDto>> Add(CreateCourseAssignment courseAssignmentDto)
    {
        var courseAssignment = mapper.Map<CourseAssignment>(courseAssignmentDto);


        await context.CourseAssignments.AddAsync(courseAssignment);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetCourseAssignmentDto>(courseAssignment);

        return result == 0
            ? new Response<GetCourseAssignmentDto>(HttpStatusCode.BadRequest, "CourseAssignment not added!")
            : new Response<GetCourseAssignmentDto>(data);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var exist = await context.CourseAssignments.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "CourseAssignment not found");
        }

        context.CourseAssignments.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "CourseAssignment not deleted!")
            : new Response<string>("CourseAssignment deleted!");
    }

    public async Task<Response<List<GetCourseAssignmentDto>>> GetAll(StudentFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var courseAssignments = context.CourseAssignments.AsQueryable();

        var mapped = mapper.Map<List<GetCourseAssignmentDto>>(courseAssignments);
        var totalRecords = mapped.Count;

        var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        return new PagedResponse<List<GetCourseAssignmentDto>>(data, validFilter.PageNumber, validFilter.PageSize,
            totalRecords);
    }

    public async Task<Response<GetCourseAssignmentDto>> GetById(int id)
    {
        var exist = await context.CourseAssignments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseAssignmentDto>(HttpStatusCode.NotFound, "CourseAssignment not found!");
        }

        var data = mapper.Map<GetCourseAssignmentDto>(exist);

        return new Response<GetCourseAssignmentDto>(data);
    }

    public async Task<Response<GetCourseAssignmentDto>> Update(int id, UpdateCourseAssignment courseAssignmentDto)
    {
        var exist = await context.CourseAssignments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseAssignmentDto>(HttpStatusCode.NotFound, "CourseAssignment not found");
        }

        exist.CourseId = courseAssignmentDto.CourseId;
        exist.InstructorId = courseAssignmentDto.InstructorId;
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetCourseAssignmentDto>(exist);

        return result == 0
            ? new Response<GetCourseAssignmentDto>(HttpStatusCode.BadRequest, "CourseAssignment not updated")
            : new Response<GetCourseAssignmentDto>(data);
    }
}
