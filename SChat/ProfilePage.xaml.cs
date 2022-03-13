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
                BirthdayBox.Text = user.Birthday.ToString();
            else
                BirthdayBox.Text = "Еще не заполнено.";
            if (user.PhoneNumber != null || user.PhoneNumber.Length != 10)
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
                user.Email = EmailBox.Text;
                cnt.db.SaveChanges();
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
                user.Birthday = Convert.ToDateTime(BirthdayBox.Text);
                cnt.db.SaveChanges();
            }
            catch
            {
                new ErrorWindow("Неверный формат.").ShowDialog();
            }
        }
        private void PhoneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                user.PhoneNumber = PhoneNumberBox.Text;
                cnt.db.SaveChanges();
            }
            catch
            {
                new ErrorWindow("Неверный формат.").ShowDialog();
            }
        }
    }
}
