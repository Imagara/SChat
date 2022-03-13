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
            Update();
            MainFrame.Content = new WelcomePage();
        }
        private void AddNewChat(object sender, RoutedEventArgs e)
        {
            AddNewStackPanel.Visibility = Visibility.Visible;
        }
        private void AddNewChatClose(object sender, RoutedEventArgs e)
        {
            AddNewStackPanel.Visibility = Visibility.Collapsed;
            if (ChatAddNameOfChat.Text.Trim() != "" && ChatAddNameOfChat.Text.Length < 50)
            {
                if (cnt.db.UserChat.Select(item => item.Chat.Name + item.User.NickName).Contains(ChatAddNameOfChat.Text + cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.NickName).FirstOrDefault()))
                    new ErrorWindow("Вы уже состоите в этом чате.").Show();
                else if (Functions.IsChatAlreadyCreated(ChatAddNameOfChat.Text))
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
                        new ErrorWindow("Ошибка вступления в уже существующий чат: " + ex).Show();
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
                        new ErrorWindow("Ошибка создания чата: " + ex).ShowDialog();
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
            chatName.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            chatName.HorizontalAlignment = HorizontalAlignment.Left;
            chatName.VerticalAlignment = VerticalAlignment.Top;
            chatName.FontSize = 10;
            chatName.Width = 90;
            chatName.MouseDown += NewChatSelected;
            textGrid.Children.Add(chatName);

            Label chatLastMessage = new Label();
            chatLastMessage.Content = chatLastMessageS;
            chatLastMessage.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            chatLastMessage.HorizontalAlignment = HorizontalAlignment.Left;
            chatLastMessage.VerticalAlignment = VerticalAlignment.Bottom;
            chatLastMessage.FontSize = 10;
            chatLastMessage.Width = 90;
            textGrid.Children.Add(chatLastMessage);

            chatGrid.Children.Add(textGrid);

            ChatListBox.Items.Add(chatGrid);
        }
        public void LoadingChat()
        {
            ChatListBox.Items.Clear();
            foreach (UserChat cht in cnt.db.UserChat.Where(chat => chat.IdUser == Profile.userId).ToList())
            {
                try
                {
                    Chat selChat = cnt.db.Chat.Where(chatt => chatt.IdChat == cht.IdChat).FirstOrDefault();
                    string chatName = selChat.Name;
                    string chatLastMessage = cnt.db.Message.Where(item => item.IdChat == cht.IdChat).OrderByDescending(order => order.IdMessage).Select(item => item.Content).FirstOrDefault();
                    if (chatLastMessage == "" || chatLastMessage == null)
                        chatLastMessage = "Чат создан.";
                    if (chatLastMessage != null && chatLastMessage.Length > 13)
                        chatLastMessage = chatLastMessage.Substring(0, 13) + "...";
                    BitmapImage chatImgSource;
                    if (selChat.ImgSource == null)
                        chatImgSource = new BitmapImage(new Uri("../Resources/StandartChat.png", UriKind.RelativeOrAbsolute));
                    else
                        chatImgSource = ImagesManip.NewImage(cnt.db.Chat.Where(item => item.IdChat == cht.IdChat).FirstOrDefault());

                    AddNewChat(chatName, chatLastMessage, chatImgSource);
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }

            try
            {
                Grid addChatGrid = new Grid();
                addChatGrid.Name = "AddNewChatButton";
                addChatGrid.Height = 40;
                addChatGrid.Width = 135;
                addChatGrid.Margin = new Thickness(0, 2, 0, 2);

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;

                Button addChatButton = new Button();
                addChatButton.Width = 30;
                addChatButton.Height = 30;
                addChatButton.Margin = new Thickness(3, 0, 3, 0);
                addChatButton.Content = new Image { Source = new BitmapImage(new Uri("../Resources/Add.png", UriKind.RelativeOrAbsolute)), Margin = new Thickness(8) };
                addChatButton.Click += AddNewChat;

                stackPanel.Children.Add(addChatButton);

                Button updateChatButton = new Button();
                updateChatButton.Width = 30;
                updateChatButton.Height = 30;
                updateChatButton.Margin = new Thickness(3, 0, 3, 0);
                updateChatButton.Content = new Image { Source = new BitmapImage(new Uri("../Resources/Refresh.png", UriKind.RelativeOrAbsolute)), Margin = new Thickness(5) };
                updateChatButton.Click += UpdateChat;

                stackPanel.Children.Add(updateChatButton);
                addChatGrid.Children.Add(stackPanel);

                ChatListBox.Items.Add(addChatGrid);
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.ToString()).ShowDialog();
            }
        }
        private void NewChatSelected(object sender, RoutedEventArgs e)
        {
            if (Profile.openedChat != Convert.ToInt32(cnt.db.Chat.Where(item => item.Name == ((Label)sender).Content.ToString()).Select(item => item.IdChat).FirstOrDefault()))
            {
                Profile.openedChat = Convert.ToInt32(cnt.db.Chat.Where(item => item.Name == ((Label)sender).Content.ToString()).Select(item => item.IdChat).FirstOrDefault());
                MainFrame.Content = new ChatPage();
            }
        }
        private void Profile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new ProfilePage();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMininize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateChat(object sender, RoutedEventArgs e)
        {
            Update();
        }
        private void Update()
        {
            User user = cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault();
            ProfileName.Content = user.NickName;
            ProfileStatus.Content = user.Status;
            if (cnt.db.User.Where(item => item.Id == Profile.userId).Select(item => item.ProfileImgSource).FirstOrDefault() == null)
                ProfileImage.Source = new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute));
            else
                ProfileImage.Source = ImagesManip.NewImage(cnt.db.User.Where(item => item.Id == Profile.userId).FirstOrDefault());
            LoadingChat();
        }
    }
}