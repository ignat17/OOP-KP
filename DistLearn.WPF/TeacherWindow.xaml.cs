using System;
using System.Windows;
using System.Windows.Controls;
using DistLearn;
using DistLearn.WPF.Data;

namespace DistLearn.WPF
{
    public partial class TeacherWindow : Window
    {
        private Teacher currentTeacher;

        public TeacherWindow()
        {
            InitializeComponent();
            DeadlinePicker.SelectedDate = DateTime.Now.AddDays(7);
            LoadTeacherData();
        }

        private void LoadTeacherData()
        {
            currentTeacher = AppData.CurrentUser as Teacher;

            if (currentTeacher == null)
            {
                MessageBox.Show("Викладача не знайдено.");
                Close();
                return;
            }

            UserText.Text = currentTeacher.FullName;
            LoadTeacherCourses();
        }

        private void LoadTeacherCourses()
        {
            CoursesList.Items.Clear();

            for (int i = 0; i < AppData.Courses.Count; i++)
            {
                Course course = AppData.Courses[i];

                if (course.Teacher != null)
                {
                    if (course.Teacher.Login == currentTeacher.Login)
                    {
                        CoursesList.Items.Add(course);
                    }
                }
            }

            if (CoursesList.Items.Count > 0)
            {
                CoursesList.SelectedIndex = 0;
            }
            else
            {
                ClearCourseInfo();
            }
        }

        private void CourseChanged(object sender, SelectionChangedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                ClearCourseInfo();
                return;
            }

            ShowCourseInfo(course);
        }

        private void ShowCourseInfo(Course course)
        {
            TitleText.Text = course.Title;
            DescText.Text = course.Description;
            StudentsText.Text = course.GetStudentsCount().ToString();

            MaterialsList.Items.Clear();
            AssignmentsList.Items.Clear();

            for (int i = 0; i < course.Contents.Count; i++)
            {
                CourseContent content = course.Contents[i];

                if (content is Material)
                {
                    Material material = (Material)content;
                    MaterialsList.Items.Add(material.Title);
                }

                if (content is Assignment)
                {
                    Assignment assignment = (Assignment)content;
                    AssignmentsList.Items.Add(assignment.Title + " (до " + assignment.Deadline.ToShortDateString() + ")");
                }
            }

            if (MaterialsList.Items.Count == 0)
            {
                MaterialsList.Items.Add("Матеріали відсутні");
            }

            if (AssignmentsList.Items.Count == 0)
            {
                AssignmentsList.Items.Add("Завдання відсутні");
            }
        }

        private void ClearCourseInfo()
        {
            TitleText.Text = "";
            DescText.Text = "";
            StudentsText.Text = "";
            MaterialsList.Items.Clear();
            AssignmentsList.Items.Clear();
        }

        private void CreateCourseBtn_Click(object sender, RoutedEventArgs e)
        {
            string title = CourseTitleBox.Text;
            string description = CourseDescBox.Text;

            if (title == "")
            {
                MessageBox.Show("Введіть назву курсу.");
                return;
            }

            Course newCourse = currentTeacher.CreateCourse(title, description);

            if (newCourse == null)
            {
                MessageBox.Show("Не вдалося створити курс.");
                return;
            }

            AppData.Courses.Add(newCourse);

            CourseTitleBox.Text = "";
            CourseDescBox.Text = "";

            LoadTeacherCourses();
            MessageBox.Show("Курс створено.");
        }

        private void AddMaterialBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            string materialTitle = MaterialTitleBox.Text;

            if (materialTitle == "")
            {
                MessageBox.Show("Введіть назву матеріалу.");
                return;
            }

            Material material = new Material();
            material.Title = materialTitle;
            material.Description = "Новий матеріал";
            material.MaterialType = "Text";
            material.Url = "https://example.com";

            course.AddContent(material);

            MaterialTitleBox.Text = "";
            ShowCourseInfo(course);

            MessageBox.Show("Матеріал додано.");
        }

        private void AddAssignmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            string assignmentTitle = AssignmentTitleBox.Text;

            if (assignmentTitle == "")
            {
                MessageBox.Show("Введіть назву завдання.");
                return;
            }

            if (DeadlinePicker.SelectedDate == null)
            {
                MessageBox.Show("Оберіть дедлайн.");
                return;
            }

            Assignment assignment = new Assignment();
            assignment.Title = assignmentTitle;
            assignment.Description = "Нове завдання";
            assignment.Instructions = "Виконати завдання та здати вчасно.";
            assignment.MaxScore = 100;
            assignment.Deadline = DeadlinePicker.SelectedDate.Value;

            course.AddContent(assignment);

            AssignmentTitleBox.Text = "";
            DeadlinePicker.SelectedDate = DateTime.Now.AddDays(7);

            ShowCourseInfo(course);

            MessageBox.Show("Завдання додано.");
        }

        private void DeleteMaterialBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            string materialTitle = MaterialsList.SelectedItem as string;

            if (materialTitle == null || materialTitle == "Матеріали відсутні")
            {
                MessageBox.Show("Оберіть матеріал.");
                return;
            }

            for (int i = 0; i < course.Contents.Count; i++)
            {
                Material material = course.Contents[i] as Material;

                if (material != null)
                {
                    if (material.Title == materialTitle)
                    {
                        course.Contents.RemoveAt(i);
                        break;
                    }
                }
            }

            ShowCourseInfo(course);
            MessageBox.Show("Матеріал видалено.");
        }

        private void DeleteAssignmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            string assignmentText = AssignmentsList.SelectedItem as string;

            if (assignmentText == null || assignmentText == "Завдання відсутні")
            {
                MessageBox.Show("Оберіть завдання.");
                return;
            }

            for (int i = 0; i < course.Contents.Count; i++)
            {
                Assignment assignment = course.Contents[i] as Assignment;

                if (assignment != null)
                {
                    if (assignmentText.StartsWith(assignment.Title))
                    {
                        course.Contents.RemoveAt(i);
                        break;
                    }
                }
            }

            ShowCourseInfo(course);
            MessageBox.Show("Завдання видалено.");
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Видалити вибраний курс?",
                "Підтвердження",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                AppData.Courses.Remove(course);
                LoadTeacherCourses();
                MessageBox.Show("Курс видалено.");
            }
        }

        private void AddStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            string login = StudentLoginBox.Text;

            if (login == "")
            {
                MessageBox.Show("Введіть логін студента.");
                return;
            }

            Student student = null;

            for (int i = 0; i < AppData.Users.Count; i++)
            {
                student = AppData.Users[i] as Student;

                if (student != null && student.Login == login)
                {
                    break;
                }

                student = null;
            }

            if (student == null)
            {
                MessageBox.Show("Студента не знайдено.");
                return;
            }

            for (int i = 0; i < course.Enrollments.Count; i++)
            {
                if (course.Enrollments[i].Student != null &&
                    course.Enrollments[i].Student.Login == student.Login)
                {
                    MessageBox.Show("Студент уже є на курсі.");
                    return;
                }
            }

            student.EnrollToCourse(course);

            if (student.Enrollments.Count > 0)
            {
                Enrollment enrollment = student.Enrollments[student.Enrollments.Count - 1];
                course.Enrollments.Add(enrollment);
            }

            StudentLoginBox.Text = "";
            ShowCourseInfo(course);

            MessageBox.Show("Студента додано.");
        }

        private void DeleteStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                MessageBox.Show("Оберіть курс.");
                return;
            }

            string login = StudentLoginBox.Text;

            if (login == "")
            {
                MessageBox.Show("Введіть логін студента.");
                return;
            }

            Student student = null;

            for (int i = 0; i < AppData.Users.Count; i++)
            {
                student = AppData.Users[i] as Student;

                if (student != null && student.Login == login)
                {
                    break;
                }

                student = null;
            }

            if (student == null)
            {
                MessageBox.Show("Студента не знайдено.");
                return;
            }

            bool deleted = false;

            for (int i = 0; i < course.Enrollments.Count; i++)
            {
                if (course.Enrollments[i].Student != null &&
                    course.Enrollments[i].Student.Login == student.Login)
                {
                    course.Enrollments.RemoveAt(i);
                    deleted = true;
                    break;
                }
            }

            for (int i = 0; i < student.Enrollments.Count; i++)
            {
                if (student.Enrollments[i].Course != null &&
                    student.Enrollments[i].Course.Title == course.Title)
                {
                    student.Enrollments.RemoveAt(i);
                    break;
                }
            }

            if (!deleted)
            {
                MessageBox.Show("Цього студента немає на курсі.");
                return;
            }

            StudentLoginBox.Text = "";
            ShowCourseInfo(course);

            MessageBox.Show("Студента видалено.");
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            AppData.CurrentUser = null;
            Close();
        }
    }
}