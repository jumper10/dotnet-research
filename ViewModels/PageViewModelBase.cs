using System.ComponentModel;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase
    {
        private bool _onLoading;
        public bool OnLoading
        {
            get { return _onLoading; }
            set { Set(() => OnLoading, ref _onLoading, value); }
        }

        protected T Get<T>()
        {
            return SimpleIoc.Default.GetInstance<T>(); 
        }

        public virtual void OnLoaded(object obj=null) { }

        public virtual void UnLoaded() { }

    }
}
