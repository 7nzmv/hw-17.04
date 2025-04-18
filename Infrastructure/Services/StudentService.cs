using System.Net;
using AutoMapper;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context, IMapper mapper) : IStudentService
{
    public async Task<Response<GetStudentDto>> AddStudent(CreateStudentDto studentDTO)
    {
        var student = mapper.Map<Student>(studentDTO);


        await context.Students.AddAsync(student);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetStudentDto>(student);

        return result == 0
            ? new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student not added!")
            : new Response<GetStudentDto>(data);
    }

    public async Task<Response<string>> DeleteStudent(int id)
    {
        var exist = await context.Students.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Student not found");
        }

        context.Students.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Student not deleted!")
            : new Response<string>("Student deleted!");
    }

    public async Task<Response<List<GetStudentDto>>> GetAll(StudentFilter filter)
    {
        var validfilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var students = context.Students.AsQueryable();

        if (filter.Name != null)
        {
            students = students.Where(s => string.Concat(s.FirstName, " ", s.LastName).ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.From != null)
        {
            var year = DateTime.UtcNow.Year;
            students = students.Where(s => year - s.Birthdate.Year >= filter.From);
        }

        var mapped = mapper.Map<List<GetStudentDto>>(students);
        var totalRecords = mapped.Count;
        var data = mapped
                .Skip((validfilter.PageNumber - 1) * validfilter.PageSize)
                .Take(validfilter.PageSize)
                .ToList();
        return new PagedResponse<List<GetStudentDto>>(data, validfilter.PageNumber, validfilter.PageSize,
                totalRecords);
    }

    public async Task<Response<GetStudentDto>> GetStudentById(int id)
    {
        var exist = await context.Students.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetStudentDto>(HttpStatusCode.NotFound, "Student not found!");
        }

        var data = mapper.Map<GetStudentDto>(exist);

        return new Response<GetStudentDto>(data);
    }

    public async Task<Response<GetStudentDto>> Update(int id, UpdateStudentDto studentDTO)
    {
        var exist = await context.Students.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetStudentDto>(HttpStatusCode.NotFound, "Student not found");
        }

        exist.FirstName = studentDTO.FirstName;
        exist.LastName = studentDTO.LastName;
        exist.Birthdate = studentDTO.BirthDate;
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetStudentDto>(exist);

        return result == 0
            ? new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student not updated")
            : new Response<GetStudentDto>(data);
    }
    

    //3
    public async Task<Response<List<StudentCourseCountDto>>> GetCourseCountPerStudent()
    {
        var data = await context.Students
            .Select(s => new StudentCourseCountDto
            {
                Student = s.FirstName + " " + s.LastName,
                CourseCount = s.Enrollments.Count
            })
            .ToListAsync();

        return new Response<List<StudentCourseCountDto>>(data);
    }
    

    //4
    public async Task<Response<List<StudentWithoutCoursesDto>>> GetStudentsWithoutCourses()
    {
        var data = await context.Students
            .Where(s => !s.Enrollments.Any())
            .Select(s => new StudentWithoutCoursesDto
            {
                Student = s.FirstName + " " + s.LastName
            })
            .ToListAsync();

        return new Response<List<StudentWithoutCoursesDto>>(data);
    }
}
