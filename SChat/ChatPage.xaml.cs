using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SChat
{
    public partial class ChatPage : Page
    {
        public ChatPage()
        {
            InitializeComponent();
            ChatName.Content = cnt.db.Chat.Where(item => item.IdChat == Profile.openedChat).Select(item => item.Name).FirstOrDefault();
            LoadingMessages();
        }
        private void SendMessageButton(object sender, RoutedEventArgs e)
        {
            Message newMessage = new Message();
            if (MsgBox.Text.Trim() != "")
            {
                newMessage = new Message()
                {
                    IdMessage = cnt.db.Message.Select(p => p.IdMessage).DefaultIfEmpty(0).Max() + 1,
                    IdUser = Profile.userId,
                    IdChat = Profile.openedChat,
                    Content = MsgBox.Text,
                    Date = DateTime.Now
                };
                cnt.db.Message.Add(newMessage);
                cnt.db.SaveChanges();
                MsgBox.Text = "";

            }

            if (MessageListBox.Items.Count > 50 || (cnt.db.Message.Where(item => item.IdChat == Profile.openedChat).OrderByDescending(order => order.IdMessage).Select(item => item.IdMessage).FirstOrDefault() - 1 != Profile.lastMessageId))
            {
                LoadingMessages();
                MessageBox.Show("Updated all" + Profile.lastMessageId);
            }
            else if (newMessage != null)
            {
                try
                {
                    int idAuthor = newMessage.User.Id;
                    string author = newMessage.User.NickName;
                    string content = newMessage.Content;
                    DateTime dt = newMessage.Date;
                    BitmapImage imgSource;
                    if (cnt.db.User.Where(item => item.Id == idAuthor).Select(item => item.ProfileImgSource).FirstOrDefault() == null)
                        imgSource = new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute));
                    else
                        imgSource = ImagesManip.NewImage(cnt.db.User.Where(item => item.Id == idAuthor).FirstOrDefault());

                    SendMessage(author, content, dt.ToString("dd.MM.yyyy HH:mm"), imgSource);
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }

            Profile.lastMessageId = cnt.db.Message.Where(item => item.IdChat == Profile.openedChat).OrderByDescending(order => order.IdMessage).Select(item => item.IdMessage).FirstOrDefault();
        }
        private void SendMessage(string nickName, string message, string date, BitmapImage imageSource)
        {
            Grid messageGrid = new Grid();
            messageGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0x44, 0x4B));
            messageGrid.Height = 45;
            messageGrid.Width = 590;
            messageGrid.Margin = new Thickness(10, 5, 10, 5);

            Image messageImage = new Image();
            messageImage.Source = imageSource;
            messageImage.Width = 35;
            messageImage.Height = 35;
            messageImage.Margin = new Thickness(5);
            messageImage.HorizontalAlignment = HorizontalAlignment.Left;
            messageGrid.Children.Add(messageImage);

            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            Label authorLabel = new Label();
            authorLabel.Content = nickName;
            authorLabel.Foreground = Brushes.White;
            authorLabel.FontWeight = FontWeights.Bold;
            authorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            authorLabel.VerticalAlignment = VerticalAlignment.Top;
            authorLabel.Margin = new Thickness(40, 0, 0, 0);

            Label dateLabel = new Label();
            dateLabel.Content = date;
            dateLabel.Foreground = Brushes.White;

            stackpanel.Children.Add(authorLabel);
            stackpanel.Children.Add(dateLabel);
            messageGrid.Children.Add(stackpanel);

            Label messageLabel = new Label();
            messageLabel.Content = message;
            messageLabel.Foreground = Brushes.White;
            messageLabel.HorizontalAlignment = HorizontalAlignment.Left;
            messageLabel.VerticalAlignment = VerticalAlignment.Bottom;
            messageLabel.Margin = new Thickness(40, 0, 0, 0);
            messageGrid.Children.Add(messageLabel);

            MessageListBox.Items.Add(messageGrid);
        }
        void LoadingMessages()
        {
            MessageListBox.Items.Clear();
            foreach (Message msg in cnt.db.Message.Where(chat => chat.IdChat == Profile.openedChat).OrderByDescending(id => id.IdMessage).Take(25).OrderBy(id => id.IdMessage).ToList())
            {
                try
                {
                    Message message = cnt.db.Message.Where(item => item.IdMessage == msg.IdMessage).FirstOrDefault();
                    int idAuthor = message.User.Id;
                    string author = message.User.NickName;
                    string content = msg.Content;
                    DateTime dt = msg.Date;
                    BitmapImage imgSource;
                    if (cnt.db.User.Where(item => item.Id == idAuthor).Select(item => item.ProfileImgSource).FirstOrDefault() == null)
                        imgSource = new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute));
                    else
                        imgSource = ImagesManip.NewImage(cnt.db.User.Where(item => item.Id == idAuthor).FirstOrDefault());

                    SendMessage(author, content, dt.ToString("dd.MM.yyyy HH:mm"), imgSource);
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }
            scroll.ScrollToEnd();
        }

        private void ChatSettings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChatSettingsPage(Profile.openedChat));
        }
        private void ChatLeave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cnt.db.UserChat.Remove(cnt.db.UserChat.Where(item => item.IdUser == Profile.userId && item.IdChat == Profile.openedChat).FirstOrDefault());
                cnt.db.SaveChanges();
                WelcomePage wp = new WelcomePage();
                NavigationService.Navigate(wp);
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.ToString()).ShowDialog();
            }

        }
    }
}
