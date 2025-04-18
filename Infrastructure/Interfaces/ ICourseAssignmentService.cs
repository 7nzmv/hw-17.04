using Domain.DTOs.CourseAssignmentDto;
using Domain.DTOs.CourseAssignmentServiceDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICourseAssignmentService
{
    Task<Response<GetCourseAssignmentDto>> Add(CreateCourseAssignment courseAssignmentDto);
    Task<Response<string>> Delete(int id);
    Task<Response<List<GetCourseAssignmentDto>>> GetAll(StudentFilter filter);
    Task<Response<GetCourseAssignmentDto>> GetById(int id);
    Task<Response<GetCourseAssignmentDto>> Update(int id, UpdateCourseAssignment courseAssignmentDto);

}
