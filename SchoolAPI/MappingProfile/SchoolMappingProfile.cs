﻿using AutoMapper;
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
                .ForMember(dst => dst.Students, map => map.MapFrom(src => src.Student));

            //CreateMap<SubjectsTaughtByTeacher, TeacherDto>()
            //    .ForMember(dst => dst.Subjects, map => map.MapFrom(src => src.Subject));

            CreateMap<Teacher, TeacherDto>();

            //CreateMap<Subject, SubjectDto>();

            CreateMap<Student, StudentDto>();

            //For HttpPost
            CreateMap<CreateClassAndTeacherDto, Class>()
                .ForMember(dst => dst.Teacher, map => map.MapFrom(src => new Teacher()
                {TeacherName=src.TeacherName, TeacherSecondName=src.TeacherSecondName,TeacherTitle=src.TeacherTitle}));


        }
    }
}
