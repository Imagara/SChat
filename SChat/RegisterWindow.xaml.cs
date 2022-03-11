using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SChat
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidLogAndPass(NickNameBox.Text, PassBox.Password))
                    new ErrorWindow("Поля не могут быть пустыми.").Show();
                else if (!Functions.IsValidLogAndPassRegister(NickNameBox.Text, PassBox.Password))
                    new ErrorWindow("Поля «Логин» и «Пароль» должны содержать не менее 5 символов. Поля «Логин» и «Пароль» не должны быть равны").Show();
                else if (Functions.IsLoginAlreadyTaken(NickNameBox.Text))
                    new ErrorWindow("Данный логин уже занят").Show();
                else if (!Functions.IsValidEmail(EmailBox.Text))
                    new ErrorWindow("Email введен неверно.").Show();
                else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                    new ErrorWindow("Данный email уже используется.").Show();
                else
                {
                    User newUser = new User()
                    {
                        Id = cnt.db.User.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1,
                        NickName = NickNameBox.Text,
                        Password = Encrypt.GetHash(PassBox.Password),
                        Email = EmailBox.Text
                    };
                    cnt.db.User.Add(newUser);
                    cnt.db.SaveChanges();;

                    new LoginWindow().Show();
                    this.Close();
                }
            }
            catch
            {
                new ErrorWindow("Ошибка.").ShowDialog();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void ButtonMininize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
