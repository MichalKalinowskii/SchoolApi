using AutoMapper;
using SchoolAPI.DTOs;
using SchoolAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.MappingProfile
{
    public class SchoolMappingProfile : Profile
    {
        public SchoolMappingProfile()
        {
            
            
            CreateMap<Class, ClassDto>()
                .ForMember(dtc => dtc.Students, map => map.MapFrom(src => src.Student));

            CreateMap<SubjectsTaughtByTeacher, SubjectsTaughtByTeacherDto>();

            CreateMap<Teacher, SubjectsTaughtByTeacherDto>()
                .ForMember(dtc=>dtc.TeacherId,map=>map.MapFrom(src=>src.TeacherId));

            CreateMap<Subject, SubjectsTaughtByTeacherDto>()
                .ForMember(dtc => dtc.SubjectId, map => map.MapFrom(src => src.SubjectId)); 

            CreateMap<Teacher, TeacherDto>();

            CreateMap<Subject, SubjectDto>();

            CreateMap<Student, StudentDto>();

            
        }
    }
}
