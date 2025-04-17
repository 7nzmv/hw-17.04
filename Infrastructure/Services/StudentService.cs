using System.Net;
using AutoMapper;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context, IMapper mapper)
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

    public async Task<Response<List<GetStudentDto>>> GetAll()
    {
        var students = await context.Students.ToListAsync();

        var data = mapper.Map<List<GetStudentDto>>(students);
        return new Response<List<GetStudentDto>>(data);
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
}
