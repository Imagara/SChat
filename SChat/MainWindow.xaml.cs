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
            LoadingChat();
        }
        
        private void AddNewChat(object sender, RoutedEventArgs e)
        {
            //
        }
        private void AddNewChat(string chatNameS, string chatLastMessageS,string chatImgSource)
        {
            Grid chatGrid = new Grid();
            chatGrid.Height = 40;
            chatGrid.Margin = new Thickness(0, 2, 0, 2);

            Image chatImg = new Image();
            chatImg.Margin = new Thickness(5,0,0,0);
            chatImg.HorizontalAlignment = HorizontalAlignment.Left;
            chatImg.Width = 30;
            chatImg.Height = 30;
            chatImg.Source = new BitmapImage(new Uri(chatImgSource));
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
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChatPage();
        }
        private void LoadingChat()
        {
            ChatListBox.Items.Clear();
            foreach (UserChat cht in cnt.db.UserChat.Where(chat => chat.IdUser == Profile.UserId).ToList())
            {
                string chatName = cnt.db.Chat.Where(chat => chat.IdChat == cht.IdChat).Select(chat => chat.Name).FirstOrDefault();
                string chatLastMessage = cnt.db.Message.Where(chat => chat.IdChat == cht.IdChat).Select(chat => chat.Content).FirstOrDefault();
                string chatImgSource = cnt.db.Chat.Where(chat => chat.IdChat == cht.IdChat).Select(chat => chat.ImgSource).FirstOrDefault();
                AddNewChat(chatName, chatLastMessage, chatImgSource);
            }
        }
        private void NewChatSelected(object sender, RoutedEventArgs e)
        {
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
        private void ChatListDoubleClick(object sender, RoutedEventArgs e)
        {
            if (ChatListBox.SelectedItem != null)
            {
                MessageBox.Show("Q" + ChatListBox.SelectedItems);
            }    
        }
    }
}
