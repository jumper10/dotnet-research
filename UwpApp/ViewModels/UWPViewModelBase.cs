using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace UwpApp.ViewModels
{
    public abstract class UWPViewModelBase : PageViewModelBase
    {
        protected NavigationService NavigationService { get; set; }

        public UWPViewModelBase()
        {
            NavigationService = SimpleIoc.Default.GetInstance<NavigationService>(ViewModelLocator.MainShellNav);
        }
    }
}
