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
    /// UCTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class UCTreeView : UserControl
    {
        public UCTreeView()
        {
            InitializeComponent();
            Loaded += UCTreeView_Loaded;
        }

        private void UCTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = new TreeViewModel();
            vm.Loaded();
            DataContext = vm;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var item= e.OriginalSource as TreeViewItem;
            item?.BringIntoView();
        }
    }
}
