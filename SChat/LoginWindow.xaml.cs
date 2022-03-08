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
                    MessageBox.Show("Поля не могут быть пустыми");
                else if (!Functions.LoginCheck(LogBox.Text, PassBox.Password))
                    MessageBox.Show("Неверный логин или пароль");
                else
                {
                    Profile.userId = cnt.db.User.Where(item => item.NickName == LogBox.Text).Select(item => item.Id).FirstOrDefault();
                    //Profile.imgSource = cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.ProfileImgSource).FirstOrDefault();
                    Profile.nickName = cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.NickName).FirstOrDefault();
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
            }
            catch
            {

            }
            

        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Close();
        }
    }
}
