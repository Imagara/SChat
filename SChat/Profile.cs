using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SChat
{
    public class Profile
    {
        public int UserId { get; set; }
        //public string NickName = { get; set; }
        public static string NickName = "Imagara";
        public static string ImgSource = cnt.db.User.First(item => item.Id == 1).ProfileImgSource;

    }
}
