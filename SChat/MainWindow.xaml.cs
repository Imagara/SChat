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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProfileName.Content = Profile.NickName;
            ProfileImage.Source = new BitmapImage(new Uri(Profile.ImgSource));
        }

        private void AddNewChat(object sender, RoutedEventArgs e)
        {

        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChatPage();
        }
        private void NewChatSelected(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChatPage();
        }
        private void Profile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new ProfilePage();
        }
    }
}
