using System.IO;
using System.Text.Json;
using System.Windows;
using DistLearn;
using DistLearn.WPF.Data;
using Microsoft.Win32;

namespace DistLearn.WPF
{
    public partial class AdminWindow : Window
    {
        private Administrator currentAdmin;

        public AdminWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            currentAdmin = AppData.CurrentUser as Administrator;

            if (currentAdmin == null)
            {
                MessageBox.Show("Адміністратора не знайдено.");
                Close();
                return;
            }

            UserText.Text = currentAdmin.FullName;
            LoadUsers();
            LoadCourses();
        }

        private void LoadUsers()
        {
            UsersList.Items.Clear();

            for (int i = 0; i < AppData.Users.Count; i++)
            {
                User user = AppData.Users[i];
                UsersList.Items.Add(user.Login + " (" + user.GetRoleName() + ")");
            }
        }

        private void LoadCourses()
        {
            CoursesList.Items.Clear();

            for (int i = 0; i < AppData.Courses.Count; i++)
            {
                CoursesList.Items.Add(AppData.Courses[i]);
            }
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PassBox.Password;
            string fullName = NameBox.Text;

            if (login == "" || password == "" || fullName == "")
            {
                MessageBox.Show("Заповніть усі поля.");
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Пароль має містити не менше 8 символів.");
                return;
            }

            for (int i = 0; i < AppData.Users.Count; i++)
            {
                if (AppData.Users[i].Login == login)
                {
                    MessageBox.Show("Користувач з таким логіном уже існує.");
                    return;
                }
            }

            if (StudentRadio.IsChecked == true)
            {
                Student student = new Student();
                student.Login = login;
                student.Password = password;
                student.FullName = fullName;
                student.GroupName = "Нова група";

                AppData.Users.Add(student);
            }
            else if (TeacherRadio.IsChecked == true)
            {
                Teacher teacher = new Teacher();
                teacher.Login = login;
                teacher.Password = password;
                teacher.FullName = fullName;
                teacher.Department = "Нова кафедра";

                AppData.Users.Add(teacher);
            }
            else if (AdminRadio.IsChecked == true)
            {
                Administrator admin = new Administrator();
                admin.Login = login;
                admin.Password = password;
                admin.FullName = fullName;

                AppData.Users.Add(admin);
            }

            LoginBox.Text = "";
            PassBox.Password = "";
            NameBox.Text = "";
            StudentRadio.IsChecked = true;

            LoadUsers();
            MessageBox.Show("Користувача додано.");
        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string selectedUserText = UsersList.SelectedItem as string;

            if (selectedUserText == null)
            {
                MessageBox.Show("Оберіть користувача.");
                return;
            }

            string login = selectedUserText.Split(' ')[0];

            for (int i = 0; i < AppData.Users.Count; i++)
            {
                if (AppData.Users[i].Login == login)
                {
                    if (AppData.Users[i].Login == currentAdmin.Login)
                    {
                        MessageBox.Show("Не можна видалити поточного адміністратора.");
                        return;
                    }

                    AppData.Users.RemoveAt(i);
                    break;
                }
            }

            LoadUsers();
            MessageBox.Show("Користувача видалено.");
        }

        private void DeleteCourseBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Видалити вибраний курс?","Підтвердження",MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                AppData.Courses.Remove(course);
                LoadCourses();
                MessageBox.Show("Курс видалено.");
            }
        }

        private void SaveJsonBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json";
            dialog.FileName = "data.json";

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            AppDataFile dataFile = new AppDataFile();

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

                    dataFile.Admins.Add(adminFile);
                }
                else if (teacher != null)
                {
                    TeacherFile teacherFile = new TeacherFile();
                    teacherFile.Login = teacher.Login;
                    teacherFile.Password = teacher.Password;
                    teacherFile.FullName = teacher.FullName;
                    teacherFile.Department = teacher.Department;

                    dataFile.Teachers.Add(teacherFile);
                }
                else if (student != null)
                {
                    StudentFile studentFile = new StudentFile();
                    studentFile.Login = student.Login;
                    studentFile.Password = student.Password;
                    studentFile.FullName = student.FullName;
                    studentFile.GroupName = student.GroupName;

                    dataFile.Students.Add(studentFile);
                }
            }

            for (int i = 0; i < AppData.Courses.Count; i++)
            {
                Course course = AppData.Courses[i];
                CourseFile courseFile = new CourseFile();

                courseFile.Title = course.Title;
                courseFile.Description = course.Description;

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

                dataFile.Courses.Add(courseFile);
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(dataFile, options);
            File.WriteAllText(dialog.FileName, json);

            MessageBox.Show("Дані успішно збережено у JSON.");
        }

        private void LoadJsonBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json";

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            string json = File.ReadAllText(dialog.FileName);

            if (json == "")
            {
                MessageBox.Show("Файл порожній.");
                return;
            }

            AppDataFile dataFile = JsonSerializer.Deserialize<AppDataFile>(json);

            if (dataFile == null)
            {
                MessageBox.Show("Помилка читання JSON.");
                return;
            }

            AppData.Users.Clear();
            AppData.Courses.Clear();

            for (int i = 0; i < dataFile.Admins.Count; i++)
            {
                Administrator admin = new Administrator();
                admin.Login = dataFile.Admins[i].Login;
                admin.Password = dataFile.Admins[i].Password;
                admin.FullName = dataFile.Admins[i].FullName;

                AppData.Users.Add(admin);
            }

            for (int i = 0; i < dataFile.Teachers.Count; i++)
            {
                Teacher teacher = new Teacher();
                teacher.Login = dataFile.Teachers[i].Login;
                teacher.Password = dataFile.Teachers[i].Password;
                teacher.FullName = dataFile.Teachers[i].FullName;
                teacher.Department = dataFile.Teachers[i].Department;

                AppData.Users.Add(teacher);
            }

            for (int i = 0; i < dataFile.Students.Count; i++)
            {
                Student student = new Student();
                student.Login = dataFile.Students[i].Login;
                student.Password = dataFile.Students[i].Password;
                student.FullName = dataFile.Students[i].FullName;
                student.GroupName = dataFile.Students[i].GroupName;

                AppData.Users.Add(student);
            }

            for (int i = 0; i < dataFile.Courses.Count; i++)
            {
                CourseFile courseFile = dataFile.Courses[i];
                Teacher teacher = null;

                for (int j = 0; j < AppData.Users.Count; j++)
                {
                    Teacher currentTeacher = AppData.Users[j] as Teacher;

                    if (currentTeacher != null)
                    {
                        if (currentTeacher.Login == courseFile.TeacherLogin)
                        {
                            teacher = currentTeacher;
                            break;
                        }
                    }
                }

                if (teacher == null)
                {
                    continue;
                }

                Course course = teacher.CreateCourse(courseFile.Title, courseFile.Description);

                if (course == null)
                {
                    continue;
                }

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
                    for (int k = 0; k < AppData.Users.Count; k++)
                    {
                        Student student = AppData.Users[k] as Student;

                        if (student != null)
                        {
                            if (student.Login == courseFile.StudentLogins[j])
                            {
                                student.EnrollToCourse(course);

                                if (student.Enrollments.Count > 0)
                                {
                                    Enrollment enrollment = student.Enrollments[student.Enrollments.Count - 1];
                                    course.Enrollments.Add(enrollment);
                                }

                                break;
                            }
                        }
                    }
                }

                AppData.Courses.Add(course);
            }

            currentAdmin = AppData.CurrentUser as Administrator;

            LoadUsers();
            LoadCourses();

            MessageBox.Show("Дані успішно завантажено з JSON.");
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            AppData.CurrentUser = null;
            Close();
        }
    }
}