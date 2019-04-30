using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data.Local.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase
    {
        protected AppLogService AppLogService { get; set; }
        public PageViewModelBase() {
            AppLogService= SimpleIoc.Default.GetInstance<AppLogService>();
        }
        [PreferredConstructor]
        public PageViewModelBase(AppLogService appLogService)
        {
            AppLogService = appLogService;
        }

        private bool _onLoading;
        public bool OnLoading
        {
            get { return _onLoading; }
            set { Set(() => OnLoading, ref _onLoading, value); }
        }

        protected T Get<T>(string key)
        {
            return SimpleIoc.Default.GetInstance<T>(key);
        }

        protected T Get<T>()
        {
            return SimpleIoc.Default.GetInstance<T>(); 
        }

        public virtual void OnLoaded(object obj=null) { }

        public virtual void UnLoaded() { }

    }
}
