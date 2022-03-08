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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
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
                    MessageBox.Show("Поля не могут быть пустыми.");
                if (!Functions.IsValidLogAndPassRegister(NickNameBox.Text, PassBox.Password))
                    MessageBox.Show("Поля «Логин» и «Пароль» должны содержать не менее 5 символов. Поля «Логин» и «Пароль» не должны быть равны");
                else if (Functions.IsLoginAlreadyTaken(NickNameBox.Text))
                    MessageBox.Show("Данный логин уже занят");
                else if (!Functions.IsValidEmail(EmailBox.Text))
                    MessageBox.Show("Email введен неверно.");
                else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                    MessageBox.Show("Данный email уже используется.");
                else
                {
                    User newUser = new User()
                    {
                        Id = cnt.db.User.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1,
                        NickName = NickNameBox.Text,
                        Password = Encrypt.GetHash(PassBox.Password),
                        Email = EmailBox.Text
                    };

                    MessageBox.Show("Успешная регистрация");
                    LoginWindow lw = new LoginWindow();
                    lw.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка.");
            }
            
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }
    }
}
