using System.Windows;
using DistLearn;

namespace DistLearn.WPF
{
    public partial class CourseWindow : Window
    {
        private Course course;

        public CourseWindow(Course selectedCourse)
        {
            InitializeComponent();
            course = selectedCourse;
            LoadCourseInfo();
        }

        private void LoadCourseInfo()
        {
            if (course == null)
            {
                MessageBox.Show("Курс не знайдено.");
                Close();
                return;
            }

            TitleText.Text = course.Title;
            DescText.Text = course.Description;
            StudentsText.Text = course.GetStudentsCount().ToString();

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
                Material material = course.Contents[i] as Material;
                Assignment assignment = course.Contents[i] as Assignment;

                if (material != null)
                {
                    MaterialsList.Items.Add(material.Title);
                }
                else if (assignment != null)
                {
                    AssignmentsList.Items.Add(
                        assignment.Title + " (до " + assignment.Deadline.ToShortDateString() + ")");
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

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}