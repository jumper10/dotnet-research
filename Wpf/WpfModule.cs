using CommonLibrary;
using GalaSoft.MvvmLight.Ioc;

namespace Wpf_research
{
    public class WpfModule :Module
    {
        static WpfModule()
        {
            SimpleIoc.Default.Register<IModule, WpfModule>();
        }
    }
}
