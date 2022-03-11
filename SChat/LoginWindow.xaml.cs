using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SChat
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            LogBox.Focus();
        }
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidLogAndPass(LogBox.Text, PassBox.Password))
                    new ErrorWindow("Поля не могут быть пустыми").Show();
                else if (!Functions.LoginCheck(LogBox.Text, PassBox.Password))
                    new ErrorWindow("Неверный логин или пароль").Show();
                else
                {
                    Profile.userId = cnt.db.User.Where(item => item.NickName == LogBox.Text).Select(item => item.Id).FirstOrDefault();
                    Profile.nickName = cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.NickName).FirstOrDefault();
                    new MainWindow().Show();
                    this.Close();
                }
            }
            catch
            {

            }
            

        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            this.Close();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
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
