using System;
using System.IO;
using System.Text.Json;
using DistLearn;

namespace DistLearn.WPF.Data
{
    public static class AppStorage
    {
        private static string filePath = Path.Combine(AppContext.BaseDirectory, "appdata.json");

        public static bool Save()
        {
            try
            {
                AppDataFile data = new AppDataFile();

                SaveUsers(data);
                SaveCourses(data);
                SaveSubmissions(data);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;

                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filePath, json);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Load()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return false;
                }

                string json = File.ReadAllText(filePath);

                if (json == "")
                {
                    return false;
                }

                AppDataFile data = JsonSerializer.Deserialize<AppDataFile>(json);

                if (data == null)
                {
                    return false;
                }

                AppData.Users.Clear();
                AppData.Courses.Clear();
                AppData.Submissions.Clear();
                AppData.CurrentUser = null;

                LoadUsers(data);
                LoadCourses(data);
                LoadSubmissions(data);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void SaveUsers(AppDataFile data)
        {
            for (int i = 0; i < AppData.Users.Count; i++)
            {
                User user = AppData.Users[i];

                Administrator admin = user as Administrator;
                Teacher teacher = user as Teacher;
                Student student = user as Student;

                if (admin != null)
                {
                    AdminFile adminFile = new AdminFile();
                    adminFile.Login = admin.Login;
                    adminFile.Password = admin.Password;
                    adminFile.FullName = admin.FullName;

                    data.Admins.Add(adminFile);
                }
                else if (teacher != null)
                {
                    TeacherFile teacherFile = new TeacherFile();
                    teacherFile.Login = teacher.Login;
                    teacherFile.Password = teacher.Password;
                    teacherFile.FullName = teacher.FullName;
                    teacherFile.Department = teacher.Department;

                    data.Teachers.Add(teacherFile);
                }
                else if (student != null)
                {
                    StudentFile studentFile = new StudentFile();
                    studentFile.Login = student.Login;
                    studentFile.Password = student.Password;
                    studentFile.FullName = student.FullName;
                    studentFile.GroupName = student.GroupName;

                    data.Students.Add(studentFile);
                }
            }
        }

        private static void SaveCourses(AppDataFile data)
        {
            for (int i = 0; i < AppData.Courses.Count; i++)
            {
                Course course = AppData.Courses[i];
                CourseFile courseFile = new CourseFile();

                courseFile.Title = course.Title;
                courseFile.Description = course.Description;
                courseFile.Icon = course.Icon;

                if (course.Teacher != null)
                {
                    courseFile.TeacherLogin = course.Teacher.Login;
                }

                for (int j = 0; j < course.Enrollments.Count; j++)
                {
                    if (course.Enrollments[j].Student != null)
                    {
                        courseFile.StudentLogins.Add(course.Enrollments[j].Student.Login);
                    }
                }

                for (int j = 0; j < course.Contents.Count; j++)
                {
                    Material material = course.Contents[j] as Material;
                    Assignment assignment = course.Contents[j] as Assignment;

                    if (material != null)
                    {
                        MaterialFile materialFile = new MaterialFile();
                        materialFile.Title = material.Title;
                        materialFile.Description = material.Description;
                        materialFile.MaterialType = material.MaterialType;
                        materialFile.Url = material.Url;
                        materialFile.FilePath = material.FilePath;

                        courseFile.Materials.Add(materialFile);
                    }
                    else if (assignment != null)
                    {
                        AssignmentFile assignmentFile = new AssignmentFile();
                        assignmentFile.Title = assignment.Title;
                        assignmentFile.Description = assignment.Description;
                        assignmentFile.Instructions = assignment.Instructions;
                        assignmentFile.MaxScore = assignment.MaxScore;
                        assignmentFile.Deadline = assignment.Deadline;

                        courseFile.Assignments.Add(assignmentFile);
                    }
                }

                data.Courses.Add(courseFile);
            }
        }

        private static void SaveSubmissions(AppDataFile data)
        {
            for (int i = 0; i < AppData.Submissions.Count; i++)
            {
                Submission submission = AppData.Submissions[i];

                if (submission.Student == null || submission.Assignment == null)
                {
                    continue;
                }

                string courseTitle = "";

                for (int j = 0; j < AppData.Courses.Count; j++)
                {
                    Course course = AppData.Courses[j];

                    for (int k = 0; k < course.Contents.Count; k++)
                    {
                        Assignment assignment = course.Contents[k] as Assignment;

                        if (assignment != null)
                        {
                            if (assignment.Title == submission.Assignment.Title)
                            {
                                courseTitle = course.Title;
                                break;
                            }
                        }
                    }

                    if (courseTitle != "")
                    {
                        break;
                    }
                }

                SubmissionFile submissionFile = new SubmissionFile();
                submissionFile.StudentLogin = submission.Student.Login;
                submissionFile.CourseTitle = courseTitle;
                submissionFile.AssignmentTitle = submission.Assignment.Title;
                submissionFile.FilePath = submission.FilePath;
                submissionFile.Comment = submission.Comment;
                submissionFile.Status = submission.Status;

                data.Submissions.Add(submissionFile);
            }
        }

        private static void LoadUsers(AppDataFile data)
        {
            for (int i = 0; i < data.Admins.Count; i++)
            {
                Administrator admin = new Administrator();
                admin.Login = data.Admins[i].Login;
                admin.Password = data.Admins[i].Password;
                admin.FullName = data.Admins[i].FullName;

                AppData.Users.Add(admin);
            }

            for (int i = 0; i < data.Teachers.Count; i++)
            {
                Teacher teacher = new Teacher();
                teacher.Login = data.Teachers[i].Login;
                teacher.Password = data.Teachers[i].Password;
                teacher.FullName = data.Teachers[i].FullName;
                teacher.Department = data.Teachers[i].Department;

                AppData.Users.Add(teacher);
            }

            for (int i = 0; i < data.Students.Count; i++)
            {
                Student student = new Student();
                student.Login = data.Students[i].Login;
                student.Password = data.Students[i].Password;
                student.FullName = data.Students[i].FullName;
                student.GroupName = data.Students[i].GroupName;

                AppData.Users.Add(student);
            }
        }

        private static void LoadCourses(AppDataFile data)
        {
            for (int i = 0; i < data.Courses.Count; i++)
            {
                CourseFile courseFile = data.Courses[i];
                Teacher teacher = FindTeacher(courseFile.TeacherLogin);

                if (teacher == null)
                {
                    continue;
                }

                Course course = teacher.CreateCourse(courseFile.Title, courseFile.Description);

                if (course == null)
                {
                    continue;
                }

                course.Icon = courseFile.Icon;

                for (int j = 0; j < courseFile.Materials.Count; j++)
                {
                    Material material = new Material();
                    material.Title = courseFile.Materials[j].Title;
                    material.Description = courseFile.Materials[j].Description;
                    material.MaterialType = courseFile.Materials[j].MaterialType;
                    material.Url = courseFile.Materials[j].Url;
                    material.FilePath = courseFile.Materials[j].FilePath;

                    course.AddContent(material);
                }

                for (int j = 0; j < courseFile.Assignments.Count; j++)
                {
                    Assignment assignment = new Assignment();
                    assignment.Title = courseFile.Assignments[j].Title;
                    assignment.Description = courseFile.Assignments[j].Description;
                    assignment.Instructions = courseFile.Assignments[j].Instructions;
                    assignment.MaxScore = courseFile.Assignments[j].MaxScore;
                    assignment.Deadline = courseFile.Assignments[j].Deadline;

                    course.AddContent(assignment);
                }

                for (int j = 0; j < courseFile.StudentLogins.Count; j++)
                {
                    Student student = FindStudent(courseFile.StudentLogins[j]);

                    if (student != null)
                    {
                        student.EnrollToCourse(course);

                        if (student.Enrollments.Count > 0)
                        {
                            Enrollment enrollment = student.Enrollments[student.Enrollments.Count - 1];
                            course.Enrollments.Add(enrollment);
                        }
                    }
                }

                AppData.Courses.Add(course);
            }
        }

        private static void LoadSubmissions(AppDataFile data)
        {
            for (int i = 0; i < data.Submissions.Count; i++)
            {
                SubmissionFile submissionFile = data.Submissions[i];

                Student student = FindStudent(submissionFile.StudentLogin);
                Assignment assignment = FindAssignment(submissionFile.CourseTitle, submissionFile.AssignmentTitle);

                if (student == null || assignment == null)
                {
                    continue;
                }

                Submission submission = student.SubmitAssignment(
                    assignment,
                    submissionFile.FilePath,
                    submissionFile.Comment);

                if (submission != null)
                {
                    submission.Status = submissionFile.Status;
                    AppData.Submissions.Add(submission);
                }
            }
        }

        private static Teacher FindTeacher(string login)
        {
            for (int i = 0; i < AppData.Users.Count; i++)
            {
                Teacher teacher = AppData.Users[i] as Teacher;

                if (teacher != null)
                {
                    if (teacher.Login == login)
                    {
                        return teacher;
                    }
                }
            }

            return null;
        }

        private static Student FindStudent(string login)
        {
            for (int i = 0; i < AppData.Users.Count; i++)
            {
                Student student = AppData.Users[i] as Student;

                if (student != null)
                {
                    if (student.Login == login)
                    {
                        return student;
                    }
                }
            }

            return null;
        }

        private static Assignment FindAssignment(string courseTitle, string assignmentTitle)
        {
            for (int i = 0; i < AppData.Courses.Count; i++)
            {
                Course course = AppData.Courses[i];

                if (course.Title == courseTitle)
                {
                    for (int j = 0; j < course.Contents.Count; j++)
                    {
                        Assignment assignment = course.Contents[j] as Assignment;

                        if (assignment != null)
                        {
                            if (assignment.Title == assignmentTitle)
                            {
                                return assignment;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}