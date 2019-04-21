
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UwpApp.Views;
using ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace UwpApp.ViewModels
{
    public class MainShellViewModel : UWPViewModelBase
    {
        readonly SystemNavigationManager SystemNavigationManager = SystemNavigationManager.GetForCurrentView();
      

        private ICommand _ItemInvokedCommand;
        public ICommand ItemInvokedCommand => this._ItemInvokedCommand ?? (this._ItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));


        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            // could also use a converter on the command parameter if you don't like
            // the idea of passing in a NavigationViewItemInvokedEventArgs
            this.NavigationService.NavigateTo(args.InvokedItemContainer.Tag?.ToString());
            UpdateBackState();
        }
        public override void OnLoaded(object obj = null)
        {
            SystemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;
            base.OnLoaded(obj);
            this.NavigationService.CurrentFrame = obj as Frame;
            this.NavigationService.NavigateTo(ViewModelLocator.LocalPage);
            UpdateBackState();
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
                UpdateBackState();
            }
        }

        private void UpdateBackState()
        {
            SystemNavigationManager.AppViewBackButtonVisibility = this.NavigationService.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
