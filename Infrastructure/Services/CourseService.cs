using System.Net;
using AutoMapper;
using Domain.DTOs.CourseDto;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(DataContext context, IMapper mapper)
{
    public async Task<Response<GetCourseDto>> AddCourse(CreateCourseDto courseDto)
    {
        var course = mapper.Map<Course>(courseDto);

        await context.Courses.AddAsync(course);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetCourseDto>(course);

        return result == 0
            ? new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Course not added!")
            : new Response<GetCourseDto>(data);
    }

    public async Task<Response<string>> DeleteCourse(int id)
    {
        var exist = await context.Courses.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Course not found");
        }

        context.Courses.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Course not deleted!")
            : new Response<string>("Course deleted!");
    }

    public async Task<Response<List<GetCourseDto>>> GetAll()
    {
        var courses = await context.Courses.ToListAsync();

        var data = mapper.Map<List<GetCourseDto>>(courses);
        return new Response<List<GetCourseDto>>(data);
    }

    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        var exist = await context.Courses.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseDto>(HttpStatusCode.NotFound, "Course not found!");
        }

        var data = mapper.Map<GetCourseDto>(exist);

        return new Response<GetCourseDto>(data);
    }

    public async Task<Response<GetCourseDto>> Update(int id, UpdateCourseDto courseDto)
    {
        var exist = await context.Courses.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseDto>(HttpStatusCode.NotFound, "Course not found");
        }

        exist.Title = courseDto.Title;
        exist.Description = courseDto.Description;
        exist.Price = courseDto.Price;
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetCourseDto>(exist);

        return result == 0
            ? new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Course not updated")
            : new Response<GetCourseDto>(data);
    }
}
