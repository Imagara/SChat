using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SChat
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProfileName.Content = Profile.nickName;
            if (cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.ProfileImgSource).FirstOrDefault() == null)
                ProfileImage.Source = new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute));
            else
                ProfileImage.Source = ImagesManip.NewImage(cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault());
            LoadingChat();
            MainFrame.Content = new WelcomePage();
        }
        
        private void AddNewChat(object sender, RoutedEventArgs e)
        {
            //
        }
        private void AddNewChat(string chatNameS, string chatLastMessageS,BitmapImage chatImgSource)
        {
            MessageBox.Show("new chat added.");
            Grid chatGrid = new Grid();
            chatGrid.Height = 40;
            chatGrid.Margin = new Thickness(0, 2, 0, 2);

            Image chatImg = new Image();
            chatImg.Margin = new Thickness(5,0,0,0);
            chatImg.HorizontalAlignment = HorizontalAlignment.Left;
            chatImg.Width = 30;
            chatImg.Height = 30;
            chatImg.Source = chatImgSource;
            chatGrid.Children.Add(chatImg);

            Grid textGrid = new Grid();
            textGrid.Margin = new Thickness(40, 0, 0, 0);

            Label chatName = new Label();
            chatName.Content = chatNameS;
            chatName.FontWeight = FontWeights.Bold;
            chatName.HorizontalAlignment = HorizontalAlignment.Left;
            chatName.VerticalAlignment = VerticalAlignment.Top;
            chatName.FontSize = 10;
            chatName.Width = 90;
            chatName.MouseDown += NewChatSelected;
            textGrid.Children.Add(chatName);

            Label chatLastMessage = new Label();
            chatLastMessage.Content = chatLastMessageS;
            chatLastMessage.HorizontalAlignment = HorizontalAlignment.Left;
            chatLastMessage.VerticalAlignment = VerticalAlignment.Bottom;
            chatLastMessage.FontSize = 10;
            chatLastMessage.Width = 90;
            textGrid.Children.Add(chatLastMessage);

            chatGrid.Children.Add(textGrid);

            Border chatBorder = new Border();
            chatBorder.BorderBrush = Brushes.Black;
            chatBorder.BorderThickness = new Thickness(1.3);
            chatGrid.Children.Add(chatBorder);

            ChatListBox.Items.Add(chatGrid);
        }
        public void LoadingChat()
        {
            MessageBox.Show("Updating...");
            ChatListBox.Items.Clear();
            foreach (UserChat cht in cnt.db.UserChat.Where(chat => chat.IdUser == Profile.userId).ToList())
            {
                try
                {
                    string chatName = cnt.db.Chat.Where(chatt => chatt.IdChat == cht.IdChat).Select(chatt => chatt.Name).FirstOrDefault();
                    string chatLastMessage = cnt.db.Message.Where(chatt => chatt.IdChat == cht.IdChat).OrderByDescending(order => order.IdMessage).Select(chatt => chatt.Content).FirstOrDefault();
                    MessageBox.Show("LAST: " + chatLastMessage);
                    if (chatLastMessage != null && chatLastMessage.Length > 10)
                        chatLastMessage = chatLastMessage.Substring(0, 10) + "...";
                    BitmapImage chatImgSource;

                    if (cnt.db.Chat.Where(item => item.IdChat == cht.IdChat).Select(item => item.ImgSource).FirstOrDefault() == null)
                        chatImgSource = new BitmapImage(new Uri("../Resources/StandartChat.png", UriKind.RelativeOrAbsolute));
                    else
                        chatImgSource = ImagesManip.NewImage(cnt.db.Chat.Where(item => item.IdChat == cht.IdChat).FirstOrDefault());

                    AddNewChat(chatName, chatLastMessage, chatImgSource);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            MessageBox.Show("Updated.");
        }
        private void NewChatSelected(object sender, RoutedEventArgs e)
        {
            Profile.openedChat = Convert.ToInt32(cnt.db.Chat.Where(item => item.Name == ((Label)sender).Content.ToString()).Select(item => item.IdChat).FirstOrDefault());
            MainFrame.Content = new ChatPage();
        }
        private void Profile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new ProfilePage();
        }
        private void TempExit(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }
    }
}