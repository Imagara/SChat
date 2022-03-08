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
            
        }

        private void SaveInfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeImageProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = ImagesManip.SelectImage();
            ProfileImage.Source = image;

            User user = cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault();
            user.ProfileImgSource = ImagesManip.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source);
            cnt.db.SaveChanges();
        }
    }
}
