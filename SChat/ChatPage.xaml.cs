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
        public int openChat = 1;
        public ChatPage()
        {
            InitializeComponent();
            LoadingMessages();
        }
        private void SendMessageButton(object sender, MouseButtonEventArgs e)
        {
            if(MsgBox.Text != "")
            {
                SendMessage(Profile.NickName, MsgBox.Text, DateTime.Now.ToString("dd.MM.yyyy HH:mm"), Profile.ImgSource);

                int messageId = cnt.db.Message.Select(p => p.IdMessage).DefaultIfEmpty(0).Max() + 1;

                Message newMessage = new Message()
                {
                    IdMessage = messageId,
                    Content = MsgBox.Text,
                    Date = DateTime.Now
                };
                cnt.db.Message.Add(newMessage);
                cnt.db.SaveChanges();

                ChatMessage newChatMessage = new ChatMessage()
                {
                    Id = cnt.db.ChatMessage.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1,
                    IdChat = openChat,
                    IdMessage = messageId
                };
                cnt.db.ChatMessage.Add(newChatMessage);
                cnt.db.SaveChanges();

                MsgBox.Text = "";
            }
            LoadingMessages();
            scroll.ScrollToEnd();
        }
        private void SendMessage(string nickName, string message, string date, string imageSource)
        {
            Grid messageGrid = new Grid();
            messageGrid.Background = Brushes.White;
            messageGrid.HorizontalAlignment = HorizontalAlignment.Left;
            messageGrid.Height = 40;
            messageGrid.Width = 624;
            messageGrid.Margin = new Thickness(10, 0, 10, 10);

            Image messageImage = new Image();
            messageImage.Source = new BitmapImage(new Uri(imageSource));
            messageImage.Width = 35;
            messageImage.Height = 35;
            messageImage.Margin = new Thickness(5);
            messageImage.HorizontalAlignment = HorizontalAlignment.Left;
            messageGrid.Children.Add(messageImage);

            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            Label authorLabel = new Label();
            authorLabel.Content = nickName;
            authorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            authorLabel.VerticalAlignment = VerticalAlignment.Top;
            authorLabel.Margin = new Thickness(40, 0, 0, 0);

            Label dateLabel = new Label();
            dateLabel.Content = date;

            stackpanel.Children.Add(authorLabel);
            stackpanel.Children.Add(dateLabel);
            messageGrid.Children.Add(stackpanel);

            Label messageLabel = new Label();
            messageLabel.Content = message;
            messageLabel.HorizontalAlignment = HorizontalAlignment.Left;
            messageLabel.VerticalAlignment = VerticalAlignment.Bottom;
            messageLabel.Margin = new Thickness(40, 0, 0, 0);
            messageGrid.Children.Add(messageLabel);
            MessageStackPanel.Children.Add(messageGrid);
        }
        private void LoadingMessages()
        {
            foreach (ChatMessage chat in cnt.db.ChatMessage.OrderByDescending(cht => cht.Id).Take(25).OrderBy(cht => cht.Id).ToList())
            {
                string author = "AUTHOR";
                string content = cnt.db.Message.Where(msg => msg.IdMessage == chat.IdMessage).Select(msg => msg.Content).FirstOrDefault();
                DateTime dt = Convert.ToDateTime(cnt.db.Message.Where(msg => msg.IdMessage == chat.IdMessage).Select(msg => msg.Date).FirstOrDefault());
                if (chat.IdChat == openChat)
                    SendMessage(author, content, dt.ToString("dd.MM.yyyy HH:mm"), Profile.ImgSource);
            }
            scroll.ScrollToEnd();
        }
        private void AddSomeMessages(object sender, RoutedEventArgs e)
        {
            for (int i = 0;i < Convert.ToInt32(CountBox.Text);i++)
            {
                string message = $"Message No.{cnt.db.ChatMessage.Select(p => p.Id).DefaultIfEmpty(0).Max()}";
                SendMessage(Profile.NickName, message, DateTime.Now.ToString("dd.MM.yyyy HH:mm"), Profile.ImgSource);
                int messageId = cnt.db.Message.Select(p => p.IdMessage).DefaultIfEmpty(0).Max() + 1;

                Message newMessage = new Message()
                {
                    IdMessage = messageId,
                    Content = message,
                    Date = DateTime.Now
                };
                cnt.db.Message.Add(newMessage);
                cnt.db.SaveChanges();

                ChatMessage newChatMessage = new ChatMessage()
                {
                    Id = cnt.db.ChatMessage.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1,
                    IdChat = openChat,
                    IdMessage = messageId
                };
                cnt.db.ChatMessage.Add(newChatMessage);
                cnt.db.SaveChanges();

            }
            LoadingMessages();
            scroll.ScrollToEnd();
        }
    }
}
