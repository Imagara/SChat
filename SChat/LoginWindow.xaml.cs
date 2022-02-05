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
            logbox.Focus();
        }
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!Functions.IsValidLogAndPass(logbox.Text, passbox.Password))
            //    MessageBox.Show("Поля не могут быть пустыми");
            //else if (!Functions.LoginCheck(logbox.Text, passbox.Password))
            //    MessageBox.Show("Неверный логин или пароль");
            //else
            //{
            Profile.UserId = 1;
            Profile.NickName = "Imagara";
            Profile.openedChat = Convert.ToInt32(chatid.Text);
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            //}

        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Close();
        }
    }
}
