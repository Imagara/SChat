using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;

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
            AddNewChatButton.Visibility = Visibility.Collapsed;
            AddNewStackPanel.Visibility = Visibility.Visible;
        }
        private void AddNewChatClose(object sender, RoutedEventArgs e)
        {
            AddNewChatButton.Visibility = Visibility.Visible;
            AddNewStackPanel.Visibility = Visibility.Collapsed;
            if(ChatAddNameOfChat.Text.Trim() != "" && ChatAddNameOfChat.Text.Length < 50)
            {
                //if(cnt.db.UserChat.Select().Contains().FirstOrDefault();
                //else
                if(Functions.IsChatAlreadyCreated(ChatAddNameOfChat.Text))
                {
                    try
                    {
                        UserChat newUserChat = new UserChat()
                        {
                            Id = cnt.db.UserChat.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1,
                            IdChat = Functions.GetIdChat(ChatAddNameOfChat.Text),
                            IdUser = Profile.userId
                        };
                        cnt.db.UserChat.Add(newUserChat);
                        cnt.db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка вступления в уже существующий чат: " + ex);
                    }
                }
                else
                {
                    try
                    {
                        int newChatId = cnt.db.Chat.Select(p => p.IdChat).DefaultIfEmpty(0).Max() + 1;
                        Chat newChat = new Chat()
                        {
                            IdChat = newChatId,
                            Name = ChatAddNameOfChat.Text,
                        };
                        cnt.db.Chat.Add(newChat);
                        cnt.db.SaveChanges();


                        UserChat newUserChat = new UserChat()
                        {
                            Id = cnt.db.UserChat.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1,
                            IdChat = newChatId,
                            IdUser = Profile.userId
                        };
                        cnt.db.UserChat.Add(newUserChat);
                        cnt.db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка создания чата: " + ex);
                    }
                }
            LoadingChat();
            }    
            ChatAddNameOfChat.Text = "";
        }
        private void AddNewChat(string chatNameS, string chatLastMessageS, BitmapImage chatImgSource)
        {
            Grid chatGrid = new Grid();
            chatGrid.Height = 40;
            chatGrid.Margin = new Thickness(0, 2, 0, 2);

            Image chatImg = new Image();
            chatImg.Margin = new Thickness(5, 0, 0, 0);
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
            ChatListBox.Items.Clear();
            foreach (UserChat cht in cnt.db.UserChat.Where(chat => chat.IdUser == Profile.userId).ToList())
            {
                try
                {
                    string chatName = cnt.db.Chat.Where(chatt => chatt.IdChat == cht.IdChat).Select(chatt => chatt.Name).FirstOrDefault();
                    string chatLastMessage = cnt.db.Message.Where(chatt => chatt.IdChat == cht.IdChat).OrderByDescending(order => order.IdMessage).Select(chatt => chatt.Content).FirstOrDefault();
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