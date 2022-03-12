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
    public partial class ChatSettingsPage : Page
    {
        int chatId;
        public ChatSettingsPage(int id)
        {
            InitializeComponent();
            chatId = id;
            Chat chat = cnt.db.Chat.Where(item => item.IdChat == chatId).FirstOrDefault();
            ChatNameLabel.Text = chat.Name;
            if (chat.ImgSource == null)
                ChatImage.Source = new BitmapImage(new Uri("../Resources/StandartChat.png", UriKind.RelativeOrAbsolute));
            else
                ChatImage.Source = ImagesManip.NewImage(chat);
            LoadingUsers();
        }

        private void ChangeImageProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void LoadingUsers()
        {
            UsersListBox.Items.Clear();
            foreach (UserChat usr in cnt.db.UserChat.Where(chat => chat.IdChat == chatId).ToList())
            {
                try
                {
                    User user = cnt.db.User.Where(item => item.Id == usr.IdUser).FirstOrDefault();
                    string userName = user.NickName;
                    string userStatus = user.Status;

                    BitmapImage userImgSource;

                    if (user.ProfileImgSource == null)
                        userImgSource = new BitmapImage(new Uri("../Resources/StandartChat.png", UriKind.RelativeOrAbsolute));
                    else
                        userImgSource = ImagesManip.NewImage(user);

                    AddNewUser(userName, userStatus, userImgSource);
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }
        }
        private void AddNewUser(string userName, string userStatus, BitmapImage userImgSource)
        {
            Grid userGrid = new Grid();
            userGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0x44, 0x4B));
            userGrid.Height = 45;
            userGrid.Width = 494;
            userGrid.Margin = new Thickness(10, 5, 10, 5);

            Image messageImage = new Image();
            messageImage.Source = userImgSource;
            messageImage.Width = 35;
            messageImage.Height = 35;
            messageImage.Margin = new Thickness(5);
            messageImage.HorizontalAlignment = HorizontalAlignment.Left;
            userGrid.Children.Add(messageImage);

            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            Label authorLabel = new Label();
            authorLabel.Content = userName;
            authorLabel.Foreground = Brushes.White;
            authorLabel.FontWeight = FontWeights.Bold;
            authorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            authorLabel.VerticalAlignment = VerticalAlignment.Top;
            authorLabel.Margin = new Thickness(40, 0, 0, 0);

            stackpanel.Children.Add(authorLabel);
            userGrid.Children.Add(stackpanel);

            Label statusLabel = new Label(); // TextBlock messageTextBlock = new TextBlock();
            statusLabel.Content = userStatus;
            //messageTextBlock.TextWrapping = TextWrapping.Wrap;
            statusLabel.Foreground = Brushes.White;
            statusLabel.HorizontalAlignment = HorizontalAlignment.Left;
            statusLabel.VerticalAlignment = VerticalAlignment.Bottom;
            statusLabel.Margin = new Thickness(40, 0, 0, 0);
            userGrid.Children.Add(statusLabel);

            UsersListBox.Items.Add(userGrid);
        }
        private void ChangeImageChat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = ImagesManip.SelectImage();
            if (image != null)
            {
                ChatImage.Source = image;
                Chat chat = cnt.db.Chat.Where(item => item.IdChat == chatId).FirstOrDefault();
                chat.ImgSource = ImagesManip.BitmapSourceToByteArray((BitmapSource)ChatImage.Source);
                cnt.db.SaveChanges();
            }
        }
    }
}
