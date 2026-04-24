using System.Windows;
using System.Windows.Controls;
using DistLearn;
using DistLearn.WPF.Data;

namespace DistLearn.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        AppData.Initialize();
        LoadCourses();

        if (CoursesList.Items.Count > 0)
        {
            CoursesList.SelectedIndex = 0;
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

    private void CoursesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Course course = CoursesList.SelectedItem as Course;

        if (course == null)
        {
            TitleText.Text = "";
            DescText.Text = "";
            TeacherText.Text = "";
            CountText.Text = "";
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

        CountText.Text = course.GetStudentsCount().ToString();
    }

    private void OpenBtn_Click(object sender, RoutedEventArgs e)
    {
        Course course = CoursesList.SelectedItem as Course;

        if (course == null)
        {
            MessageBox.Show("Оберіть курс.");
            return;
        }

        string teacherName;

        if (course.Teacher != null)
        {
            teacherName = course.Teacher.FullName;
        }
        else
        {
            teacherName = "Не вказано";
        }

        MessageBox.Show(
            "Назва: " + course.Title + "\n\n" +
            "Опис: " + course.Description + "\n\n" +
            "Викладач: " + teacherName,
            "Курс");
    }

    private void LoginBtn_Click(object sender, RoutedEventArgs e)
    {
        LoginWindow loginWindow = new LoginWindow();
        loginWindow.Owner = this;
        loginWindow.ShowDialog();

        if (AppData.CurrentUser == null)
        {
            return;
        }

        Student student = AppData.CurrentUser as Student;
        Teacher teacher = AppData.CurrentUser as Teacher;
        Administrator admin = AppData.CurrentUser as Administrator;

        if (student != null)
        {
            this.Hide();

            StudentWindow studentWindow = new StudentWindow();
            studentWindow.Owner = this;
            studentWindow.ShowDialog();

            this.Show();
            LoadCourses();
            AppData.CurrentUser = null;
        }
        else if (teacher != null)
        {
            this.Hide();

            TeacherWindow teacherWindow = new TeacherWindow();
            teacherWindow.Owner = this;
            teacherWindow.ShowDialog();

            this.Show();
            LoadCourses();
            AppData.CurrentUser = null;
        }
        else if (admin != null)
        {
            this.Hide();

            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Owner = this;
            adminWindow.ShowDialog();

            this.Show();
            LoadCourses();
            AppData.CurrentUser = null;
        }
    }

    private void RegBtn_Click(object sender, RoutedEventArgs e)
    {
        RegisterWindow registerWindow = new RegisterWindow();
        registerWindow.Owner = this;
        registerWindow.ShowDialog();
    }
}