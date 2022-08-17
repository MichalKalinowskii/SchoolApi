using SchoolAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Interfaces
{
    public interface ISchoolService
    {
        IEnumerable<ClassDto> GetAllClasses();
        ClassDto GetClassById(int classId);
        IEnumerable<StudentDto> GetAllStudents();
        StudentDto GetStudentById(int studentId);
        IEnumerable<StudentDto> GetStudentsByAge(int age);
        IEnumerable<TeacherDto> GetAllTeachers();
        TeacherDto GetTeacherById(int teacherId);
        IEnumerable<TeacherDto> GetTeachersBySubject(string subject);
        IEnumerable<SubjectDto> GetAllSubjects();
        IEnumerable<int> GetAllClassesId();

        int CreateClassAndTeacherToIt(CreateClassAndTeacherDto group);
        int CreateTeacherAndSubjectsToHim(CreateTeacherAndSubjectDto teacher);
        int CreateStudentAndAssignClassToHim(CreateStudentAndAssignClassDto student);
        
        bool RemoveStudent(int studentId);
        bool RemoveTeacherThatIsNotATutor(int teacherId);

        bool UpdateTeacher(UpdateTeacherDto dto,int teacherId);
    }
}
