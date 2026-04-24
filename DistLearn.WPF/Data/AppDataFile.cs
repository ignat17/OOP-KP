using System;
using System.Collections.Generic;

namespace DistLearn.WPF.Data
{
    public class AppDataFile
    {
        public List<AdminFile> Admins {get; set;} = new List<AdminFile>();
        public List<TeacherFile> Teachers {get; set;} = new List<TeacherFile>();
        public List<StudentFile> Students {get; set;} = new List<StudentFile>();
        public List<CourseFile> Courses {get; set;} = new List<CourseFile>();
    }

    public class AdminFile
    {
        public string Login {get; set;}
        public string Password {get; set;}
        public string FullName {get; set;}
    }

    public class TeacherFile
    {
        public string Login {get; set;}
        public string Password {get; set;}
        public string FullName {get; set;}
        public string Department {get; set;}
    }

    public class StudentFile
    {
        public string Login {get; set;}
        public string Password {get; set;}
        public string FullName {get; set;}
        public string GroupName {get; set;}
    }

    public class CourseFile
    {
        public string Title {get; set;}
        public string Description {get; set;}
        public string TeacherLogin {get; set;}
        public List<string>StudentLogins{get; set;} = new List<string>();
        public List<MaterialFile>Materials{get; set;} = new List<MaterialFile>();
        public List<AssignmentFile>Assignments{get; set;} = new List<AssignmentFile>();
    }

    public class MaterialFile
    {
        public string Title {get; set;}
        public string Description {get; set;}
        public string MaterialType {get; set;}
        public string Url {get; set;}
        public string FilePath {get; set;}
    }

    public class AssignmentFile
    {
        public string Title {get; set;}
        public string Description {get; set;}
        public string Instructions {get; set;}
        public int MaxScore {get; set;}
        public DateTime Deadline {get; set;}
    }
}