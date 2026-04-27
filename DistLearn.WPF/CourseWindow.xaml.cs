using System.Windows;
using DistLearn;
using System.Windows.Input;

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

        private void MaterialsList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string materialTitle = MaterialsList.SelectedItem as string;

            if (materialTitle == null || materialTitle == "Матеріали відсутні")
            {
                return;
            }

            for (int i = 0; i < course.Contents.Count; i++)
            {
                Material material = course.Contents[i] as Material;

                if (material != null)
                {
                    if (material.Title == materialTitle)
                    {
                        MaterialWindow window = new MaterialWindow(material);
                        window.Owner = this;
                        window.ShowDialog();
                        break;
                    }
                }
            }
        }

        private void AssignmentsList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string assignmentText = AssignmentsList.SelectedItem as string;

            if (assignmentText == null || assignmentText == "Завдання відсутні")
            {
                return;
            }

            for (int i = 0; i < course.Contents.Count; i++)
            {
                Assignment assignment = course.Contents[i] as Assignment;

                if (assignment != null)
                {
                    if (assignmentText.StartsWith(assignment.Title))
                    {
                        AssignmentWindow window = new AssignmentWindow(assignment);
                        window.Owner = this;
                        window.ShowDialog();
                        break;
                    }
                }
            }
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}