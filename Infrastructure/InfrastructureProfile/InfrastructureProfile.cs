using AutoMapper;
using Domain.DTOs.CourseAssignmentServiceDto;
using Domain.DTOs.CourseDto;
using Domain.DTOs.EnrollmentDto;
using Domain.DTOs.InstructorDto;
using Domain.DTOs.StudentDto;
using Domain.Entities;

namespace Infrastructure.InfrastructureProfile;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Student,GetStudentDto>();
        CreateMap<Course,GetCourseDto>();
        CreateMap<Instructor,GetInstructorDto>();
        CreateMap<Enrollment,GetEnrollmentDto>();
        CreateMap<CourseAssignment,GetCourseAssignmentDto>();

        CreateMap<Course, CourseAverageGradeDto>()
            .ForMember(dest => dest.AverageGrade, opt => opt.MapFrom(src => src.Enrollments.Any() ? src.Enrollments.Average(e => e.Grade) : 0));
    }
}
