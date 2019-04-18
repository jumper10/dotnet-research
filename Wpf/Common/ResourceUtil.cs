using System.Windows;

namespace Wpf_research.Common
{
    public class ResourceUtil
    {
        public static object GetResource(string key)
        {
            if (Application.Current.Resources.Contains(key))
                return Application.Current.Resources[key];
            else
                return null;
        }

        public static T GetResource<T>(string key)
        {
            var resourceValue = GetResource(key);
            return resourceValue == null ? default(T) : (T)resourceValue;
        }
    }
}
