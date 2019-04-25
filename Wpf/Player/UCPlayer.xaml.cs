using Microsoft.Win32;
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

namespace Wpf_research
{
    /// <summary>
    /// UCPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class UCPlayer : UserControl
    {
        public UCPlayer()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var ofd =new  OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                this.MediaElement.Source = new Uri(ofd.FileName);
                
                this.MediaElement.Play();
            }
        }
    }
}
