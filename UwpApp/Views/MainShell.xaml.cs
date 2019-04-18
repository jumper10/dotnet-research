using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace UwpApp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainShell : Page
    {
        public MainShell()
        {
            this.InitializeComponent();
           
            Loaded += (sender, e) => {

                var currentView = SystemNavigationManager.GetForCurrentView();
                currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

                contentFrame.Navigate(typeof(DiscoverPage));
            };
        }

        private void NavView_PaneOpened(NavigationView sender, object args)
        {
           // contentFrame.Margin = new Thickness(sender.OpenPaneLength,0,0,0);
        }

        private void NavView_PaneClosed(NavigationView sender, object args)
        {
          //  contentFrame.Margin = new Thickness(sender.CompactPaneLength, 0, 0, 0);
        }
    }
}
