using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonLibrary.Utils
{
    public class PathUtil
    {
        public static string GetAppPath()
        {
            return AppContext.BaseDirectory;
        }

        public static string GetAppBaseDirectory(bool withoutAppx=false)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (withoutAppx)
            {
                if (baseDir.EndsWith(@"\AppX\") || baseDir.EndsWith(@"\AppX"))
                {
                    var dirInfo = Directory.GetParent(baseDir);
                    return dirInfo.FullName;
                }
            }
            return baseDir;
        }

        public static string GetAppDataPath(bool autoCreate=false)
        {
            var path = Path.Combine(GetAppBaseDirectory(true), "Data");
            if (autoCreate && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static string GetAppAssetsPath(bool autoCreate = false)
        {
            var path = Path.Combine(GetAppBaseDirectory(true), "Assets");
            if (autoCreate && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public static string CombineOnAppPath(params string[] paths)
        {
            var path = Path.Combine(GetAppPath(), Path.Combine(paths));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static string CombineOnAppDataPath(params string[] paths)
        {
            var path = Path.Combine(GetAppDataPath(), Path.Combine(paths));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

    }
}
