using System.Windows;
using DistLearn;
using DistLearn.WPF.Data;

namespace DistLearn.WPF;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void EnterBtn_Click(object sender, RoutedEventArgs e)
    {
        string login = LoginBox.Text;
        string password = PassBox.Password;

        if (login == "" || password == "")
        {
            MessageBox.Show("Заповніть усі поля.");
            return;
        }

        User foundUser = null;

        for (int i = 0; i < AppData.Users.Count; i++)
        {
            if (AppData.Users[i].Login == login && AppData.Users[i].Password == password)
            {
                foundUser = AppData.Users[i];
                break;
            }
        }

        if (foundUser == null)
        {
            MessageBox.Show("Невірний логін або пароль.");
            return;
        }

        AppData.CurrentUser = foundUser;

        MessageBox.Show("Вхід успішний.");
        Close();
    }

    private void CancelBtn_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}