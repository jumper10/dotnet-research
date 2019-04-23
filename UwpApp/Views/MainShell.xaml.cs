using Microsoft.Toolkit.Uwp.UI.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpApp.ViewModels;
using ViewModels;
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
        public MainShellViewModel ViewModel
        {
            get { return this.DataContext as MainShellViewModel; }
        }

        public MainShell()
        {
            this.InitializeComponent();
           
            Loaded += (sender, e) => {
               
                if (ViewModel != null)
                {
                    ViewModel.OnLoaded(contentFrame);
                }
                TraceBarController.AddHandler(UIElement.PointerPressedEvent,new PointerEventHandler(TraceBarController_PointerPressed), true);
               
            };

            Unloaded += (sender, e) =>
            {
                if (ViewModel != null)
                {
                    ViewModel.UnLoaded();
                }
            };
        }

        private void TraceBarController_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.OnChangePosition = true;
            }
            TraceBarController.CapturePointer(e.Pointer);
            TraceBarController.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(TraceBarController_PointerReleased), true);
               
        }

        private void TraceBarController_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                TraceBarController.RemoveHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(TraceBarController_PointerReleased));
                if (ViewModel != null)
                {
                    ViewModel.TraceValueChanged(ViewModel.CurrentSeconds);
                }
            }
            finally
            {
                TraceBarController.ReleasePointerCapture(e.Pointer);                
                if (ViewModel != null)
                {
                    ViewModel.OnChangePosition = false;
                }
            }
        }
    }
}
