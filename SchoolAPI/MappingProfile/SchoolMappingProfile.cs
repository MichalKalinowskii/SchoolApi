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
            //For HttpGet
            CreateMap<Class, ClassDto>()
                .ForMember(dtc => dtc.Students, map => map.MapFrom(src => src.Student));

            CreateMap<SubjectsTaughtByTeacher, SubjectsTaughtByTeacherDto>()
                .ForMember(dtc => dtc.Teacher, map => map.MapFrom(src => src.Teacher))
                .ForMember(dtc => dtc.Subject , map => map.MapFrom(src => src.Subject));

            CreateMap<SubjectsTaughtByTeacher, TeacherDto> ()
                .ForMember(dtc => dtc.TeacherId, map => map.MapFrom(src => src.TeacherId));               

            CreateMap<SubjectsTaughtByTeacher, SubjectDto>()
                .ForMember(dtc => dtc.SubjectId, map => map.MapFrom(src => src.SubjectId)); 

            CreateMap<Teacher, TeacherDto>();

            CreateMap<Subject, SubjectDto>();

            CreateMap<Student, StudentDto>();

            //For HttpPost
            CreateMap<CreateClassAndTeacherDto, Class>()
                .ForMember(dtc => dtc.Teacher, map => map.MapFrom(src => new Teacher()
                {TeacherName=src.TeacherName, TeacherSecondName=src.TeacherSecondName,TeacherTitle=src.TeacherTitle}));


        }
    }
}
