using CommonLibrary;
using Data.Local.Common;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UwpApp.ViewModels;
using UwpApp.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpApp
{
    class BootStrap
    {
        public void Run(LaunchActivatedEventArgs e)
        {
            Config();
            Migrate();
            InitShell(e);
        }

        void InitShell(LaunchActivatedEventArgs e)
        {
            InitSetting();
            InitAppNavService(e);

            if (e.PrelaunchActivated == false)
            {
                //if (rootFrame.Content == null)
                //{
                //    // 当导航堆栈尚未还原时，导航到第一页，
                //    // 并通过将所需信息作为导航参数传入来配置
                //    // 参数
                //    rootFrame.Navigate(typeof(MainShell), e.Arguments);
                //}

                var appNav = SimpleIoc.Default.GetInstance<NavigationService>(ViewModelLocator.AppNav);
                appNav.NavigateTo(ViewModelLocator.MainShell);
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }
        }
       

        private void InitSetting()
        {
            ApplicationView.PreferredLaunchViewSize = ApplicationContext.WindowLaunchSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = ApplicationContext.TitleBarBackgroud;
            titleBar.ForegroundColor = ApplicationContext.TitleBarForegroud;
            titleBar.InactiveForegroundColor = ApplicationContext.TitleBarForegroud;
            titleBar.ButtonBackgroundColor = ApplicationContext.TitleBarButtonBackground;
            titleBar.ButtonHoverBackgroundColor = ApplicationContext.TitleBarButtonHoverBackgroud;
            titleBar.InactiveBackgroundColor = ApplicationContext.InactiveTitleBarBackgroud;
            titleBar.ButtonInactiveBackgroundColor = ApplicationContext.InactiveTitleBarButtonBackground;
            titleBar.ButtonHoverForegroundColor = ApplicationContext.TitleBarForegroud;
            titleBar.ButtonInactiveForegroundColor = ApplicationContext.TitleBarForegroud;
        }

        void InitAppNavService(LaunchActivatedEventArgs e)
        {
            var nav = SimpleIoc.Default.GetInstance<NavigationService>(ViewModelLocator.AppNav);
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }
            nav.CurrentFrame = rootFrame;
        }

        void Config()
        {
            ModuleManager.ConfigAllModules();
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        void Migrate()
        {
            var dbFactory = SimpleIoc.Default.GetInstance<DbContextFactory>();
            dbFactory.Migrate();
        }

    }
}
