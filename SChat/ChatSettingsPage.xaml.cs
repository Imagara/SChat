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
        public ChatSettingsPage(int chatId)
        {
            InitializeComponent();
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
            foreach (UserChat cht in cnt.db.UserChat.Where(chat => chat.IdUser == Profile.userId).ToList())
            {
                try
                {
                    string userName = "";
                    string userStatus = "";

                    BitmapImage userImgSource;

                    if ("" == null)
                        userImgSource = new BitmapImage(new Uri("../Resources/StandartChat.png", UriKind.RelativeOrAbsolute));
                    else
                        userImgSource = ImagesManip.NewImage(cnt.db.Chat.Where(item => item.IdChat == cht.IdChat).FirstOrDefault());

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

        }
    }
}
