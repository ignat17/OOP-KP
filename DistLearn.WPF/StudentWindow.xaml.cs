using System.Windows;
using System.Windows.Controls;
using DistLearn;
using DistLearn.WPF.Data;

namespace DistLearn.WPF
{
    public partial class StudentWindow : Window
    {
        private Student currentStudent;

        public StudentWindow()
        {
            InitializeComponent();
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            if (AppData.CurrentUser == null)
            {
                MessageBox.Show("Користувач не знайдений.");
                Close();
                return;
            }

            currentStudent = AppData.CurrentUser as Student;

            if (currentStudent == null)
            {
                MessageBox.Show("Це не студент.");
                Close();
                return;
            }

            UserText.Text = currentStudent.FullName;

            CoursesList.Items.Clear();

            for (int i = 0; i < AppData.Courses.Count; i++)
            {
                Course course = AppData.Courses[i];

                for (int j = 0; j < course.Enrollments.Count; j++)
                {
                    if (course.Enrollments[j].Student != null)
                    {
                        if (course.Enrollments[j].Student.Login == currentStudent.Login)
                        {
                            CoursesList.Items.Add(course);
                            break;
                        }
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

        private void CoursesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Course course = CoursesList.SelectedItem as Course;

            if (course == null)
            {
                ClearCourseInfo();
                return;
            }

            TitleText.Text = course.Title;
            DescText.Text = course.Description;

            if (course.Teacher != null)
            {
                TeacherText.Text = course.Teacher.FullName;
            }
            else
            {
                TeacherText.Text = "Не вказано";
            }

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
            TeacherText.Text = "";

            MaterialsList.Items.Clear();
            AssignmentsList.Items.Clear();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            AppData.CurrentUser = null;
            Close();
        }
    }
}