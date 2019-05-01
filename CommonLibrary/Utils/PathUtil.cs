using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonLibrary.Utils
{
    public class PathUtil
    {
        static IAppFolder appFolder;

        static PathUtil()
        {
            try
            {
                appFolder = SimpleIoc.Default.GetInstance<IAppFolder>();
            }
            catch { }
        }

        public static string GetAppLocalPath()
        {
            return appFolder==null? AppDomain.CurrentDomain.BaseDirectory: appFolder.AppLocalFolder;
        }

        public static string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public static string CombineOnAppLocalPath(params string[] paths)
        {
            var path = Path.Combine(GetAppLocalPath(), Path.Combine(paths));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
