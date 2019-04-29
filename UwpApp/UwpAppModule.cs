using CommonLibrary;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpApp.ViewModels;
using UwpApp.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpApp
{
    public class UwpAppModule : Module
    {
        static UwpAppModule()
        {
            SimpleIoc.Default.Register<IModule, UwpAppModule>();
        }

        public override void Inital()
        {
            base.Inital();
            RegistAppNavService();
            SimpleIoc.Default.Register<NavigationService>();

            DispatcherHelper.Initialize();
        }

        void RegistAppNavService()
        {
            var nav = new NavigationService();

            nav.Configure(ViewModelLocator.MainShell, typeof(MainShell));
            nav.Configure(ViewModelLocator.PlayerPage, typeof(PlayerPage));

            SimpleIoc.Default.Register<NavigationService>(() => nav, ViewModelLocator.AppNav);
        }

       
    }
}
