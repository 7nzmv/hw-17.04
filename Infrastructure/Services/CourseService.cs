using System.Net;
using AutoMapper;
using Domain.DTOs.CourseDto;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(DataContext context, IMapper mapper) : ICourseService
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

    public async Task<Response<List<GetCourseDto>>> GetAll(StudentFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var courses = context.Courses.AsQueryable();

        var mapped = mapper.Map<List<GetCourseDto>>(courses);
        var totalRecords = mapped.Count;

        var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        return new PagedResponse<List<GetCourseDto>>(data, validFilter.PageNumber, validFilter.PageSize,
            totalRecords);
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

    //1
    public async Task<Response<List<CourseStudentCountDto>>> GetStudentCountPerCourse()
    {
        var data = await context.Courses
            .Select(c => new CourseStudentCountDto
            {
                Course = c.Title,
                StudentCount = c.Enrollments.Count
            })
            .ToListAsync();

        return new Response<List<CourseStudentCountDto>>(data);
    }
    
    //2
    public async Task<Response<List<CourseAverageGradeDto>>> GetCoursesAverageGrades()
    {
        var courses = await context.Courses
            .Include(c => c.Enrollments)
            .ToListAsync();

        if (courses.Count == 0)
        {
            return new Response<List<CourseAverageGradeDto>>(HttpStatusCode.NotFound, "No courses found");
        }

        var result = mapper.Map<List<CourseAverageGradeDto>>(courses);

        return new Response<List<CourseAverageGradeDto>>(result);
    }
}
