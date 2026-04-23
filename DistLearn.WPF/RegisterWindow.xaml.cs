using System.Windows;
using System.Windows.Controls;
using DistLearn;
using DistLearn.WPF.Data;

namespace DistLearn.WPF
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
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

            string role = "";

            if (StudentRadio.IsChecked == true)
            {
                role = "Student";
            }
            else if (TeacherRadio.IsChecked == true)
            {
                role = "Teacher";
            }

            if (role == "Student")
            {
                Student student = new Student();
                student.Login = login;
                student.Password = password;
                student.FullName = fullName;
                student.GroupName = "Нова група";

                AppData.Users.Add(student);
            }
            else if (role == "Teacher")
            {
                Teacher teacher = new Teacher();
                teacher.Login = login;
                teacher.Password = password;
                teacher.FullName = fullName;
                teacher.Department = "Нова кафедра";

                AppData.Users.Add(teacher);
            }

            MessageBox.Show("Реєстрація успішна.");
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}