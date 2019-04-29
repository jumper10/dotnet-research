using GalaSoft.MvvmLight.Ioc;
using System.Linq;

namespace CommonLibrary
{
    public static class ModuleManager
    {
        public static void ConfigAllModules()
        {
            var modules = SimpleIoc.Default.GetAllInstances<IModule>();
            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    module.Inital();
                }
            }
        }

        public static void OnloadAllModules()
        {
            var modules = SimpleIoc.Default.GetAllInstances<IModule>();
            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    module.OnLoad();
                }
            }
        }

        public static void UnloadAllModules()
        {
            var modules = SimpleIoc.Default.GetAllInstances<IModule>();
            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    module.UnLoad();
                }
            }
        }
    }
}
