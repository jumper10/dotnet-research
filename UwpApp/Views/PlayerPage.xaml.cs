using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace UwpApp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PlayerPage : Page
    {
        public PlayerPageViewModel ViewModel
        {
            get { return DataContext as PlayerPageViewModel; }
        }

        public PlayerPage()
        {
            this.InitializeComponent();
            this.Loaded += (seder, e) =>
            {
                if (ViewModel != null)
                    ViewModel.OnLoaded();
                if (ViewModel != null)
                    ViewModel.InitPlayer(this, _fileName);
            };
            Unloaded += (sender, e) =>
            {
                if (ViewModel != null)
                    ViewModel.UnLoaded();
            };
        }

        string _fileName;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is String)
            {
                _fileName =(string) e.Parameter;
                //if (ViewModel != null)
                //    ViewModel.InitPlayer(this, (string)e.Parameter);
            }
        }
    }

    public class PlayerPageNavigationEventArgs
    {
        MediaPlayer MediaPlayer { get; set; }
    }
}
