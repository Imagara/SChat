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
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        public ChatPage()
        {
            InitializeComponent();
        }
        int mess = 0;
        private void SendMessage(object sender, MouseButtonEventArgs e)
        {
            if(MessageBox.Text != "")
            {
                Grid message = new Grid();
                message.Background = Brushes.White;
                message.HorizontalAlignment = HorizontalAlignment.Left;
                message.Width = 624;
                message.Height = 40;
                message.Margin = new Thickness(10, 0, 10, 10);
                Label authorLabel = new Label();
                authorLabel.Content = Profile.NickName;
                authorLabel.HorizontalAlignment = HorizontalAlignment.Left;
                authorLabel.VerticalAlignment = VerticalAlignment.Top;
                message.Children.Add(authorLabel);
                Label messageLabel = new Label();
                messageLabel.Content = MessageBox.Text;
                messageLabel.HorizontalAlignment = HorizontalAlignment.Left;
                messageLabel.VerticalAlignment = VerticalAlignment.Bottom;
                message.Children.Add(messageLabel);
                MessageStackPanel.Children.Add(message);
                MessageBox.Text = "";
            }
        }
        private void SendMessageTest(object sender, MouseButtonEventArgs e)
        {
            mess++;
            Grid message = new Grid();
            message.Background = Brushes.White;
            message.Width = 200;
            message.Height = 40;
            message.Margin = new Thickness(0, 0, 0, 10);
            Label authorLabel = new Label();
            authorLabel.Content = $"AuthTest {mess}";
            authorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            authorLabel.VerticalAlignment = VerticalAlignment.Top;
            message.Children.Add(authorLabel);
            Label messageLabel = new Label();
            messageLabel.Content = $"MessagTest {mess}";
            messageLabel.HorizontalAlignment = HorizontalAlignment.Left;
            messageLabel.VerticalAlignment = VerticalAlignment.Bottom;
            message.Children.Add(messageLabel);

            MessageStackPanel.Children.Add(message);
        }
    }
}
