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
            ProfileImage.Source = new BitmapImage(new Uri(Profile.ImgSource));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Grid adf = new Grid();
            //adf.Margin = new Thickness(10);
            //adf.Width = 130;
            //adf.Height = 40;
            //Image afsd = new Image();
            //afsd.Margin = image.Margin;
            //afsd.HorizontalAlignment = image.HorizontalAlignment;
            //afsd.Width = image.Width;
            //afsd.Height = image.Height;
            //afsd.Source = image.Source;
            //adf.Children.Add(afsd);

            //dsg.Children.Add(adf);
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
            //MainFrame.Content = new ChatPage();
        }
    }
}
