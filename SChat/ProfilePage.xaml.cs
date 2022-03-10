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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SChat
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            User user = cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault();
            EmailBox.Content = user.Email;
            BirthdayBox.Content = user.Birthday.ToString();
            PhoneBox.Text = user.PhoneNumber;
            if (cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.ProfileImgSource).FirstOrDefault() == null)
                ProfileImage.Source = new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute));
            else
                ProfileImage.Source = ImagesManip.NewImage(cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault());
        }

        private void SaveInfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeImageProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = ImagesManip.SelectImage();
            ProfileImage.Source = image;
            //MainWindow mw = new MainWindow();
            //mw.ProfileImage.Source = image;
            User user = cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault();
            if(ProfileImage.Source != null)
            user.ProfileImgSource = ImagesManip.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source);
            cnt.db.SaveChanges();
        }
    }
}
