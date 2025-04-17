using System.Net;
using AutoMapper;
using Domain.DTOs.InstructorDto;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class InstructorService(DataContext context, IMapper mapper)
{
    public async Task<Response<GetInstructorDto>> AddInstructor(CreateInstructorDto instructorDto)
    {
        var instructor = mapper.Map<Instructor>(instructorDto);
       

        await context.Instructors.AddAsync(instructor);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetInstructorDto>(instructor);

        return result == 0
            ? new Response<GetInstructorDto>(HttpStatusCode.BadRequest, "Instructor not added!")
            : new Response<GetInstructorDto>(data);
    }

    public async Task<Response<string>> DeleteInstructor(int id)
    {
        var exist = await context.Instructors.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Instructor not found");
        }

        context.Instructors.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Instructor not deleted!")
            : new Response<string>("Student deleted!");
    }

    public async Task<Response<List<GetInstructorDto>>> GetAll()
    {
        var instructors = await context.Instructors.ToListAsync();

        var data = mapper.Map<List<GetInstructorDto>>(instructors);
        return new Response<List<GetInstructorDto>>(data);
    }

    public async Task<Response<GetInstructorDto>> GetInstructorById(int id)
    {
        var exist = await context.Instructors.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetInstructorDto>(HttpStatusCode.NotFound, "Instructor not found!");
        }

        var data = mapper.Map<GetInstructorDto>(exist);

        return new Response<GetInstructorDto>(data);
    }

    public async Task<Response<GetInstructorDto>> Update(int id, UpdateInstructorDto instructorDto)
    {
        var exist = await context.Instructors.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetInstructorDto>(HttpStatusCode.NotFound, "Instructor not found");
        }

        exist.FirstName = instructorDto.FirstName;
        exist.LastName = instructorDto.LastName;
        exist.Phone = instructorDto.Phone;
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetInstructorDto>(exist);

        return result == 0
            ? new Response<GetInstructorDto>(HttpStatusCode.BadRequest, "Instructor not updated")
            : new Response<GetInstructorDto>(data);
    }
}
