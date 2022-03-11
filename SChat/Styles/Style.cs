using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SChat
{
    public partial class Style : ResourceDictionary
    {
        private void MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Border)sender).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x2F, 0x31, 0x36));
        }

        private void MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Border)sender).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x36, 0x3C));
        }
    }
}