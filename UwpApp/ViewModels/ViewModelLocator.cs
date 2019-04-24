using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using UwpApp.Views;

namespace UwpApp.ViewModels
{
    public class ViewModelLocator
    {
        public static readonly string AppNav = "AppNav";
        public static readonly string MainShellNav = "MainShellNav";

        public static readonly string MainShell = "MainShell";
        public static readonly string DiscoverPage = "DiscoverPage";
        public static readonly string DownloadPage = "DownloadPage";
        public static readonly string FriendPage = "FriendPage";
        public static readonly string LocalPage = "LocalPage";
        public static readonly string MVPage = "MVPage";
        public static readonly string RecentlyPage = "RecentlyPage";
        public static readonly string SearchPage = "SearchPage";

        public static readonly string PlayerPage = "PlayerPage";

        public ViewModelLocator()
        {

            SimpleIoc.Default.Register<UWPViewModelBase>(()=>new MainShellViewModel(), MainShell);
            SimpleIoc.Default.Register<UWPViewModelBase>(() => new LocalPageViewModel(), LocalPage);
            SimpleIoc.Default.Register<UWPViewModelBase>(() => new DiscoverPageViewModel(), DiscoverPage);
            SimpleIoc.Default.Register<UWPViewModelBase>(() => new FriendPageViewModel(), FriendPage);
            SimpleIoc.Default.Register<UWPViewModelBase>(() => new MVPageViewModel(), MVPage);
            SimpleIoc.Default.Register<UWPViewModelBase>(() => new RecentlyPageViewModel(), RecentlyPage);
            SimpleIoc.Default.Register<UWPViewModelBase>(() => new SearchPageViewModel(), SearchPage);

            SimpleIoc.Default.Register<UWPViewModelBase>(() => new PlayerPageViewModel(), PlayerPage);


            var nav = new NavigationService();
            ConfigNavigationService(nav);
            SimpleIoc.Default.Register<NavigationService>(() => nav, ViewModelLocator.MainShellNav);
        }

        public UWPViewModelBase GetViewModel(string vmKey)
        {
            return SimpleIoc.Default.GetInstance<UWPViewModelBase>(vmKey);
        }

        public MainShellViewModel MainShellViewModel
        {
            get { return GetViewModel(MainShell) as MainShellViewModel; }
        }

        public LocalPageViewModel LocalPageViewModel
        {
            get { return GetViewModel(LocalPage) as LocalPageViewModel; }
        }
        public DiscoverPageViewModel DiscoverPageViewModel
        {
            get { return GetViewModel(DiscoverPage) as DiscoverPageViewModel; }
        }

        public FriendPageViewModel FriendPageViewModel
        {
            get { return GetViewModel(FriendPage) as FriendPageViewModel; }
        }
        public MVPageViewModel MVPageViewModel
        {
            get { return GetViewModel(MVPage) as MVPageViewModel; }
        }

        public RecentlyPageViewModel RecentlyPageViewModel
        {
            get { return GetViewModel(RecentlyPage) as RecentlyPageViewModel; ; }
        }
        public SearchPageViewModel SearchPageViewModel
        {
            get { return GetViewModel(SearchPage) as SearchPageViewModel; ; }
        }

        public PlayerPageViewModel PlayerPageViewModel
        {
            get { return GetViewModel(PlayerPage) as PlayerPageViewModel; ; }
        }

        private void ConfigNavigationService(NavigationService nav)
        {
            if (nav == null) return;
            nav.Configure(DiscoverPage, typeof(DiscoverPage));
            nav.Configure(DownloadPage, typeof(DownloadPage));
            nav.Configure(FriendPage, typeof(FriendPage));
            nav.Configure(LocalPage, typeof(LocalPage));
            nav.Configure(MVPage, typeof(MVPage));
            nav.Configure(RecentlyPage, typeof(RecentlyPage));
            nav.Configure(SearchPage, typeof(SearchPage));

           
        }


        // <summary>
        // The cleanup.
        // </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
