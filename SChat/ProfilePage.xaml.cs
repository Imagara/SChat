using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SChat
{
    public partial class ProfilePage : Page
    {
        User user = cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault();
        public ProfilePage()
        {
            InitializeComponent();
            NickNameBox.Text = user.NickName;
            StatusBox.Text = user.Status;
            if (user.Email != null)
                EmailBox.Text = user.Email;
            else
                EmailBox.Text = "Еще не заполнено.";
            if (user.Birthday != null)
            {
                DateTime Birthday = (DateTime)user.Birthday;
                BirthdayBox.Text = Birthday.ToLongDateString();
            }
            else
                BirthdayBox.Text = "Еще не заполнено.";
            if (user.PhoneNumber != null && user.PhoneNumber.Length != 11)
                PhoneNumberBox.Text = "+7(" + user.PhoneNumber.Substring(0, 3) + ")" + user.PhoneNumber.Substring(3, 3) + "-" + user.PhoneNumber.Substring(6, 2) + "-" + user.PhoneNumber.Substring(8, 2);
            else
                PhoneNumberBox.Text = "Еще не заполнено.";
            if (cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.ProfileImgSource).FirstOrDefault() == null)
                ProfileImage.Source = new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute));
            else
                ProfileImage.Source = ImagesManip.NewImage(cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault());
        }
        private void ChangeImageProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = ImagesManip.SelectImage();
            if (image != null)
            {
                ProfileImage.Source = image;
                user.ProfileImgSource = ImagesManip.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source);
                cnt.db.SaveChanges();
            }
        }

        private void SaveProfileInfo_Click(object sender, RoutedEventArgs e)
        {
            User user = cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault();

            if (!Functions.IsValidLength(NickNameBox.Text.Trim()))
                new ErrorWindow("Поле «Никнейм» должно содержать не менее 5 символов.").Show();
            else if (Functions.IsLoginAlreadyTaken(NickNameBox.Text) && NickNameBox.Text != user.NickName)
                new ErrorWindow("Данный логин уже занят").Show();
            else
            {
                user.NickName = NickNameBox.Text;
                user.Status = StatusBox.Text;
                new ErrorWindow("Успешно").ShowDialog();
                cnt.db.SaveChanges();
            }
        }

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidEmail(EmailBox.Text))
                    new ErrorWindow("Email введен неверно.").Show();
                else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                    new ErrorWindow("Данный email уже используется.").Show();
                else
                {
                    user.Email = EmailBox.Text;
                    cnt.db.SaveChanges();
                    new ErrorWindow("Успешно").ShowDialog();
                }
            }
            catch
            {
                new ErrorWindow("Неверный формат.").ShowDialog();
            }
        }

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidDateOfBirthday(Convert.ToDateTime(BirthdayBox.Text)))
                    new ErrorWindow("Дата рождения не может быть равна сегодняшней.").Show();
                else
                {
                    user.Birthday = Convert.ToDateTime(BirthdayBox.Text);
                    cnt.db.SaveChanges();
                    new ErrorWindow("Успешно").ShowDialog();
                }
            }
            catch
            {
                new ErrorWindow("Неверный формат.").ShowDialog();
            }
        }
        private void PhoneButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PhoneNumberBox.Text[0] != '8')
                    new ErrorWindow("Номер телефона должен начинать с 8").Show();
                else if (!Functions.IsValidPhoneNumber(PhoneNumberBox.Text))
                    new ErrorWindow("Номер телефона должен содержать в себе 11 цифр.").Show();
                else if (Functions.IsPhoneNumberAlreadyTaken(PhoneNumberBox.Text))
                    new ErrorWindow("Данный номер телефона уже используется.").Show();
                else
                {
                    user.PhoneNumber = PhoneNumberBox.Text.Substring(1, 10);
                    cnt.db.SaveChanges();
                    new ErrorWindow("Успешно").ShowDialog();
                }

            }
            catch (Exception ex)
            {
                new ErrorWindow("Неверный формат.").ShowDialog();
            }
        }
    }
}
