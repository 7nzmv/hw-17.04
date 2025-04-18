using Domain.DTOs.EnrollmentDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IEnrollmentService
{
    Task<Response<GetEnrollmentDto>> AddEnrollment(CreateEnrrollmentDto enrrollmentDto);
    Task<Response<string>> DeleteEnrollment(int id);
    Task<Response<List<GetEnrollmentDto>>> GetAll(StudentFilter filter);
    Task<Response<GetEnrollmentDto>> GetEnrollmentById(int id);
    Task<Response<GetEnrollmentDto>> Update(int id, UpdateEnrollmentDto enrollmentDto);
}
