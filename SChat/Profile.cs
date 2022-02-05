using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SChat
{
    public class Profile
    {
        public static int UserId { get; set; }
        public static string NickName { get; set; }
        public static string ImgSource = cnt.db.User.First(item => item.Id == 1).ProfileImgSource;
        public static int openedChat { get; set; }
    }
}
